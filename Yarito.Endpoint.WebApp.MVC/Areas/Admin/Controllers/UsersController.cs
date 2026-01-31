using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Cities.AppServices;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Controllers
{
    [Area(nameof(Areas.Admin))]
    [Authorize(Roles = "Admin")]
    [LogActivity]
    public class UsersController(
        IAppUserAppServices appUserAppServices,
        IAuthenticationAppServices authenticationAppServices,
        IRequestAppServices requestAppServices,
        IBidAppServices bidAppServices,
        ICityAppServices cityAppServices,
        IMapper mapper) : Controller
    {
        private void Notification<T>(Result<T> result)
        {
            var r = result.Status switch
            {
                ResultStatusEnum.Failure => Result<string>.Failure(result.Message),
                ResultStatusEnum.Warning => Result<string>.Warning(result.Message),
                _ => Result<string>.Success(result.Message),
            };
            TempData["Notification"] = JsonConvert.SerializeObject(r);
        }

        public async Task<IActionResult> Index(
            CancellationToken ct, int page = 1, string? search = null, UserTypeEnum? userType = null)
        {
            var result = await appUserAppServices.GetAppUserSummaryListAsync(new AppUserReqDto()
            {
                PageSize = 5,
                TextSearch = search,
                UserType = userType,
                Page = page
            }, ct);

            var model = mapper.Map<UsersViewModel>(result);
            model.Search = search;
            model.UserType = userType;

            return View(model);
        }

        public async Task<IActionResult> CustomerDetails(int id, CancellationToken ct, int page = 1, string? search = null)
        {
            var userResult = await appUserAppServices.GetAppUserFullByIdAsync(id, ct);
            if(userResult.Status != ResultStatusEnum.Success || userResult.Data is null || userResult.Data.UserType != UserTypeEnum.Customer)
                return RedirectToAction("Index");

            Notification(userResult);
            var requestResult = await requestAppServices.GetRequestsSummaryListAsync(new RequestReqDto()
            {
                PageSize = 5,
                TextSearch = search,
                CustomerId = id,
                Page = page,
                
            }, ct);

            var model = mapper.Map<CustomerDetailsViewModel>(requestResult);
            model.UserDetails = userResult.Data;
            model.Search = search;

            return View(model);
        }

        public async Task<IActionResult> ExpertDetails(
            int id, CancellationToken ct, int page = 1, string? search = null)
        {
            var userResult = await appUserAppServices.GetAppUserFullByIdAsync(id, ct);
            if (userResult.Status != ResultStatusEnum.Success || userResult.Data is null || userResult.Data.UserType != UserTypeEnum.Expert)
                return RedirectToAction("Index");

            Notification(userResult);
            var expertWorksResult = await appUserAppServices.GetExpertCategoryWorksListDto(id, ct);
            var bidResult = await bidAppServices.GetBidsSummaryListAsync(new BidReqDto()
            {
                PageSize = 5,
                TextSearch = search,
                ExpertId = id,
                Page = page,
            }, ct);

            var model = mapper.Map<ExpertDetailsViewModel>(bidResult);
            model.UserDetails = userResult.Data;
            model.ExpertWorks = expertWorksResult;
            model.ExpertId = id;
            model.Search = search;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await appUserAppServices.SoftDeleteAsync(id, ct);
            Notification(result);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserForm(CancellationToken ct)
        {
            var model = new UserFormViewModel()
            {
                CityList = await cityAppServices.GetAllAsync(ct)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserForm(UserFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                model.CityList = await cityAppServices.GetAllAsync(ct);
                return View(model);
            }

            var registerDto = mapper.Map<RegisterDto>(model);
            var result = await authenticationAppServices.RegisterAsync(registerDto, ct);

            Notification(result);
            if (result.Status == ResultStatusEnum.Success)
                return RedirectToAction("Index", "Users", new { area = "Admin" });

            model.CityList = await cityAppServices.GetAllAsync(ct);
            return View(model);
        }
    }
}
