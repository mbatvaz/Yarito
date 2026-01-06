using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Controllers;

[Area(nameof(Areas.Admin))]
public class ReviewsController(IReviewsAppServices reviewsAppServices) : Controller
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
        CancellationToken ct, int page = 1, string? search = null, ReviewStatusEnum? status = null)
    {
        var result = await reviewsAppServices.GetReviewsListAsync(new ReviewReqDto
        {
            PageSize = 10,
            TextSearch = search,
            ApprovalStatus = status,
            Page = page
        }, ct);

        var model = new ReviewsViewModel
        {
            Reviews = result.Items,
            Search = search,
            Status = status,
            Page = result.Page,
            TotalCount = result.TotalCount,
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)result.PageSize)
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id, CancellationToken ct)
    {
        var result = await reviewsAppServices.ApproveAsync(id, ct);
        Notification(result);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id, CancellationToken ct)
    {
        var result = await reviewsAppServices.RejectAsync(id, ct);
        Notification(result);
        return RedirectToAction(nameof(Index));
    }
}
