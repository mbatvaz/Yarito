using Microsoft.AspNetCore.Mvc;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Controllers
{
    [Area(nameof(Areas.Admin))]
    public class DashboardController(
        IAppUserAppServices appUserAppServices,
        IReviewsAppServices reviewsAppServices,
        IRequestAppServices requestAppServices) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var statistics = await appUserAppServices.GetStatisticsAsync(ct);

            var reviewsList = await reviewsAppServices
                .GetReviewsListAsync(new ReviewReqDto
            {
                ApprovalStatus = ReviewStatusEnum.Pending,
                PageSize = 6
            }, ct);

            var requestsList = await requestAppServices
                .GetRequestsSummaryListAsync(new RequestReqDto
            {
                PageSize = 5
            }, ct);
            var model = new DashboardViewModel
            {
                AppStatistics = statistics,
                CommentsAwaitingApproval = reviewsList.Items,
                RequestsSummary = requestsList.Items
            };
            return View(model);
        }
    }
}
