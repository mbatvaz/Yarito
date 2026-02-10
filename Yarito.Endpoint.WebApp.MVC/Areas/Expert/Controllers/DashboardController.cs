using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Controllers
{
    [Area(nameof(Areas.Expert))]
    [Authorize(Roles = "Expert")]
    [LogActivity]
    public class DashboardController(
        IAppUserAppServices appUserAppServices,
        IBidAppServices bidAppServices,
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

        public async Task<IActionResult> Index(CancellationToken ct, int page = 1)
        {
            var userId = GetUserId();
            var userResult = await appUserAppServices.GetAppUserDashboardByIdAsync(userId, ct);

            if (userResult.Status != ResultStatusEnum.Success || userResult.Data is null)
            {
                Notification(userResult);
                return RedirectToAction("Logout", "Authentication", new { area = "Account" });
            }

            var visitsResult = await bidAppServices.GetExpertBids(new BidReqDto()
            {
                ExpertId = userId,
                Status = BidStatusEnum.Pending,
                PreferredFrom = DateTime.Today,
                PreferredTo = DateTime.Today.AddDays(1).AddTicks(-1),
                Page = page,
                PageSize = 4

            }, ct);

            var expertWorks = await appUserAppServices.GetExpertCategoryWorksListDto(userId, ct);

            var model = new DashboardViewModel()
            {
                UserInfo = userResult.Data,
                TodayVisits = visitsResult.Items,
                ExpertWorks = expertWorks,

                PageSize = visitsResult.PageSize,
                Page = visitsResult.Page,
                TotalCount = visitsResult.TotalCount
            };

            return View(model);
        }

        public async Task<IActionResult> MyBids(int page = 1, BidStatusEnum? status = null, string ? search = null,  CancellationToken ct = default)
        {
            var result = await bidAppServices.GetExpertBids(new BidReqDto()
            {
                ExpertId = GetUserId(),
                TextSearch = search,
                Status = status,
                Page = page,
                PageSize = 6,
            }, ct);

            var model = new MyBidsViewModel()
            {
                Bid = result.Items,

                PageSize = result.PageSize,
                Page = result.Page,
                TotalCount = result.TotalCount
            };

            return View(model);
        }
    }
}
