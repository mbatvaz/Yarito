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

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Controllers
{
    [Area(nameof(Areas.Admin))]
    public class UsersController(
        IAppUserAppServices appUserAppServices,
        IAuthenticationAppServices authenticationAppServices,
        IRequestAppServices requestAppServices,
        IBidAppServices bidAppServices,
        ICityAppServices cityAppServices) : Controller
    {
        private void Notification(Result<string> r) => TempData["Notification"] = JsonConvert.SerializeObject(r);

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

            var model = new UsersViewModel()
            {
                UserList = result.Items,
                UserType = userType,
                Search = search,
                Page = page,
                TotalCount = result.TotalCount,
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)result.PageSize)
            };
            return View(model);
        }

        public async Task<IActionResult> CustomerDetails(
            int id, CancellationToken ct, int page = 1, string? search = null)
        {
            var userResult = await appUserAppServices.GetAppUserFullByIdAsync(id, ct);
            if(userResult.Status != ResultStatusEnum.Success || userResult.Data is null || userResult.Data.UserType != UserTypeEnum.Customer)
            {
                Notification(Result<string>.Warning(userResult.Message));
                return RedirectToAction("Index");
            }

            var requestResult = await requestAppServices.GetRequestsSummaryListAsync(new RequestReqDto()
            {
                PageSize = 5,
                TextSearch = search,
                CustomerId = id,
                Page = page,
                
            }, ct);

            var model = new CustomerDetailsViewModel()
            {
                UserDetails = userResult.Data,
                Requests = requestResult.Items,
                CustomerId = id,
                Search = search,
                Page = page,
                TotalCount = requestResult.TotalCount,
                TotalPages = (int)Math.Ceiling(requestResult.TotalCount / (double)requestResult.PageSize)
            };
            return View(model);
        }

        public async Task<IActionResult> ExpertDetails(
            int id, CancellationToken ct, int page = 1, string? search = null)
        {
            var userResult = await appUserAppServices.GetAppUserFullByIdAsync(id, ct);
            if (userResult.Status != ResultStatusEnum.Success || userResult.Data is null || userResult.Data.UserType != UserTypeEnum.Expert)
            {
                Notification(Result<string>.Warning(userResult.Message));
                return RedirectToAction("Index");
            }

            var expertWorksResult = await appUserAppServices.GetExpertCategoryWorksListDto(id, ct);
            var bidResult = await bidAppServices.GetBidsSummaryListAsync(new BidReqDto()
            {
                PageSize = 5,
                TextSearch = search,
                ExpertId = id,
                Page = page,
            }, ct);

            var model = new ExpertDetailsViewModel()
            {
                UserDetails = userResult.Data,
                ExpertWorks = expertWorksResult,
                Bids = bidResult.Items,
                ExpertId = id,
                Search = search,
                Page = page,
                TotalCount = bidResult.TotalCount,
                TotalPages = (int)Math.Ceiling(bidResult.TotalCount / (double)bidResult.PageSize)
            };
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

            var result = await authenticationAppServices.RegisterAsync(new RegisterDto()
            {
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                PhoneNumber = model.PhoneNumber!,
                Password = model.Password!,
                UserType = model.UserType!.Value,
                Address = model.Address,
                BaseWalletBalance = model.BaseWalletBalance,
                CityId = model.CityId,
                Email = model.Email,
                ProfileImage = model.ProfileImage?.OpenReadStream(),
                ProfileImageUrl = model.ProfileImage != null ? Path.GetExtension(model.ProfileImage.FileName) : null
            }, ct);

            Notification(result);
            if (result.Status == ResultStatusEnum.Success)
                return RedirectToAction("Index", "Users", new { area = "Admin" });

            model.CityList = await cityAppServices.GetAllAsync(ct);
            return View(model);
        }
    }
}
