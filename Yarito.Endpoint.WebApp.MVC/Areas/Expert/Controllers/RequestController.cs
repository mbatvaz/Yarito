using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.AppServices.Works;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Controllers
{
    [Area(nameof(Areas.Expert))]
    [Authorize(Roles = "Expert")]
    [LogActivity]
    public class RequestController(
        IRequestAppServices requestAppServices,
        IWorkAppServices workAppServices,
        IBidAppServices bidAppServices,
        IAppUserAppServices appUserAppServices,
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
        public async Task<IActionResult> Open(int page = 1, int? workId = null, string? search = null, CancellationToken ct = default)
        {
            var result = await requestAppServices.GetOpenRequestAsync(new RequestReqDto()
            {
                ExpertId = GetUserId(),
                WorkId = workId,
                TextSearch = search,
                Page = page,
                PageSize = 6
            }, ct);

            if (result.Status != ResultStatusEnum.Success)
            {
                Notification(result);
                return RedirectToAction("Index", "Dashboard", new { area = "Expert" });
            }
            var expertWork = await workAppServices.GetExpertWorks(GetUserId(), ct);

            var model = new OpenRequestViewModel
            {
                ExpertWorks = expertWork,
                OpenRequest = result.Data.Items,
                WorkId = workId,
                Search = search,
                PageSize = 6,
                Page = result.Data.Page,
                TotalCount = result.Data.TotalCount
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int requestId, CancellationToken ct)
        {
            var request = await requestAppServices.GetRequestFullByIdAsync(requestId, ct);
            if (TempData["Notification"] is null)
                Notification(request);
            if (request.Status != ResultStatusEnum.Success || request.Data is null)
            {
                Notification(request);
                return RedirectToAction(nameof(Open));
            }
            var customer = await appUserAppServices.GetAppUserSummaryByIdAsync(request.Data.CustomerId, ct);

            if (customer.Status != ResultStatusEnum.Success || customer.Data is null)
            {
                Notification(request);
                return RedirectToAction(nameof(Open));
            }

            var bid = await bidAppServices.GetExpertBidForRequestAsync(requestId, GetUserId(), ct);

            var model = new DetailsViewModel()
            {
                Request = request.Data,
                Customer = customer.Data,
                Bid = bid.Data,
                RequestId = request.Data.Id,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBid(DetailsViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var request = await requestAppServices.GetRequestFullByIdAsync(model.RequestId, ct);
                if (request.Status != ResultStatusEnum.Success || request.Data is null)
                {
                    Notification(request);
                    return RedirectToAction(nameof(Open));
                }

                var customer = await appUserAppServices.GetAppUserSummaryByIdAsync(request.Data.CustomerId, ct);
                if (customer.Status != ResultStatusEnum.Success || customer.Data is null)
                {
                    Notification(customer);
                    return RedirectToAction(nameof(Open));
                }

                var bid = await bidAppServices.GetExpertBidForRequestAsync(model.RequestId, GetUserId(), ct);

                model.Request = request.Data;
                model.Customer = customer.Data;
                model.Bid = bid.Data;

                return View("Details", model);
            }

            var addNewBidDto = new AddNewBidDto
            {
                RequestId = model.RequestId,
                ExpertId = GetUserId(),
                ProposedPrice = model.ProposedPrice,
                ProposedVisitDateTime = model.ProposedVisitDateTime,
                Description = model.Description
            };

            var result = await bidAppServices.AddNewBidAsync(addNewBidDto, ct);
            Notification(result);

            return RedirectToAction(nameof(Details), new { requestId = model.RequestId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int requestId, int bidId, CancellationToken ct)
        {
            var result = await bidAppServices.DeleteAsync(requestId, bidId, GetUserId(), ct);
            Notification(result);
            return RedirectToAction(nameof(Details), new { requestId = requestId });
        }
    }
}
