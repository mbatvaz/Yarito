using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Controllers
{
    [Area(nameof(Areas.Customer))]
    [Authorize(Roles = "Customer")]
    [LogActivity]
    public class DashboardController(
        IAppUserAppServices appUserAppServices,
        IRequestAppServices requestAppServices,
        UserManager<IdentityUser<int>> userManager) : Controller
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

        private int GetUserId()
        {
            var userIdStr = userManager.GetUserId(User);
            return userIdStr is null 
                ? 0 
                : int.Parse(userIdStr);
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var userResult = await appUserAppServices.GetAppUserSummaryByIdAsync(GetUserId(), ct);

            if (userResult.Status != ResultStatusEnum.Success && userResult.Data is null)
            {
                Notification(userResult);
                return RedirectToAction("Logout", "Authentication", new { area = "Account" });
            }

            var requestsResult = await requestAppServices.GetRequestsSummaryListAsync(new RequestReqDto()
            {
                CustomerId = GetUserId(),
                Page = 1,
                PageSize = 5,
                FirstStatus = RequestStatusEnum.Pending,
                SecondStatus = RequestStatusEnum.InProgress
            }, ct);

            var model = new DashboardViewModel()
            {
                UserInfo = userResult.Data,
                ActiveRequests = requestsResult.Items
            };
            return View(model);
        }
    }
}
