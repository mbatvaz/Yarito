using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Cities.AppServices;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Controllers;

[Area(nameof(Areas.Admin))]
[Authorize(Roles = "Admin")]
[LogActivity]
public class RequestsController(
    IRequestAppServices requestAppServices,
    IBidAppServices bidAppServices,
    ICityAppServices cityAppServices) : Controller
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
        CancellationToken ct, int page = 1, string? search = null, RequestStatusEnum? status = null, int? cityId = null)
    {
        var result = await requestAppServices.GetRequestsCardListAsync(new RequestReqDto
        {
            PageSize = 6,
            TextSearch = search,
            FirstStatus = status,
            CityId = cityId,
            Page = page
        }, ct);

        var model = new RequestsViewModel
        {
            Requests = result.Items,
            Cities = await cityAppServices.GetAllAsync(ct),
            Search = search,
            Status = status,
            CityId = cityId,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)result.PageSize)
        };

        return View(model);
    }

    public async Task<IActionResult> RequestDetails(int id, CancellationToken ct)
    {
        var request = await requestAppServices.GetRequestFullByIdAsync(id, ct);

        if (request is null)
        {
            Notification(Result<string>.Failure("درخواست مورد نظر یافت نشد."));
            return RedirectToAction(nameof(Index));
        }

        var bidsResult = await bidAppServices.GetBidsSummaryListAsync(new BidReqDto
        {
            RequestId = id,
            TextSearch = null,
            Page = 1,
            PageSize = 10
        }, ct);

        var model = new RequestDetailsViewModel
        {
            CustomerInfo = request.CustomerInfo,
            RequestInfo = request,
            RequestImagesPath = request.RequestImagesPath.ToList(),
            AcceptedBid = request.AcceptedBid,
            BidList = bidsResult.Items,
            Search = null,
            Page = bidsResult.Page,
            PageSize = bidsResult.PageSize,
            TotalCount = bidsResult.TotalCount,
            TotalPages = (int)Math.Ceiling(bidsResult.TotalCount / (double)bidsResult.PageSize)
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeStatus(int id, RequestStatusEnum status, CancellationToken ct)
    {
        var result = await requestAppServices.ChangeStatusAsync(id, status, ct);
        Notification(result);
        return RedirectToAction(nameof(RequestDetails), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> RejectBid(int id, int requestId, CancellationToken ct)
    {
        var result = await bidAppServices.RejectBidAsync(id, ct);
        Notification(result);
        return RedirectToAction(nameof(RequestDetails), new { id = requestId });
    }

    public async Task<IActionResult> BidDetails(int id, CancellationToken ct)
    {
        var bidDetails = await bidAppServices.GetBidDetailsAsync(id, ct);
        Notification(bidDetails);

        if (bidDetails.Data is null)
            return RedirectToAction(nameof(Index));

        var model = new BidDetailsViewModel
        {
            Customer = bidDetails.Data.Customer,
            Expert = bidDetails.Data.Expert,
            Request = bidDetails.Data.Request,
            Bid = bidDetails.Data.Bid
        };

        return View(model);
    }
}
