using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        IRequestAppServices requestAppServices,
        IBidAppServices bidAppServices) : Controller
    {
        public async Task<IActionResult> Index(
            CancellationToken ct,
            int page = 1,
            string? search = null,
            UserTypeEnum? userType = null)
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
            int id,
            CancellationToken ct,
            int page = 1,
            string? search = null)
        {
            var customerResult = await appUserAppServices.GetAppUserFullByIdAsync(id, ct);
            if( customerResult.Status != ResultStatusEnum.Success 
                || customerResult.Data is null 
                || customerResult.Data.UserType != UserTypeEnum.Customer)
            {
                TempData["Notification"] = JsonConvert.SerializeObject(Result<string>.Warning(customerResult.Message));
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
                Customer = customerResult.Data,
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
            int id,
            CancellationToken ct,
            int page = 1,
            string? search = null)
        {
            var expertResult = await appUserAppServices.GetAppUserFullByIdAsync(id, ct);
            if (expertResult.Status != ResultStatusEnum.Success
                || expertResult.Data is null
                || expertResult.Data.UserType != UserTypeEnum.Expert)
            {
                TempData["Notification"] = JsonConvert.SerializeObject(Result<string>.Warning(expertResult.Message));
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
                Expert = expertResult.Data,
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
            TempData["Notification"] = JsonConvert.SerializeObject(result);
            return RedirectToAction("Index");
        }
    }
}
