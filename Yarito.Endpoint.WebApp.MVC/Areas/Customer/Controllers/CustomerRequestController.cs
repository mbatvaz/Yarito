using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Images;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Controllers
{
    [Area(nameof(Areas.Customer))]
    [Authorize(Roles = "Customer")]
    [LogActivity]
    public class CustomerRequestController(
        IAppUserAppServices appUserAppServices,
        ICategoryAppServices categoryAppServices,
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

        public async Task<IActionResult> NewRequest(CancellationToken ct)
        {
            if (!await appUserAppServices.IsCitySetAsync(GetUserId(),ct))
            {
                Notification(Result<bool>.Warning("ابتدا پروفایل خود را تکمیل کنید"));
                return RedirectToAction("Index", "Dashboard", new { area = "Customer" });
            }
            var categories = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto()
            {
                Page = 1,
                PageSize = 100
            }, ct);

            var model = new NewRequestViewModel()
            {
                CategoryList = categories.Items,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewRequest(NewRequestViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var categories = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto()
                {
                    Page = 1,
                    PageSize = 100
                }, ct);

                model.CategoryList = categories.Items;

                return View(model);
            }

            if (!await appUserAppServices.IsCitySetAsync(GetUserId(), ct))
            {
                Notification(Result<bool>.Warning("ابتدا پروفایل خود را تکمیل کنید"));
                return RedirectToAction("Index", "Dashboard", new { area = "Customer" });
            }

            var requestImages = (model.RequestImg ?? [])
                .Where(f => f is { Length: > 0 })
                .TakeLast(4)
                .Select(f => new ImageStreamDto()
                {
                    ImageStream = f.OpenReadStream(),
                    FileFormat = Path.GetExtension(f.FileName),
                    FileName = f.FileName
                })
                .ToList();

            var newRequest = new RequestNewDto()
            {
                UserId = GetUserId(),
                Title = model.Title,
                Description = model.Description,
                WorkId = model.WorkId,
                UseProfileAddress = model.UseProfileAddress,
                Address = model.Address,
                PreferredVisitDateTime = model.PreferredVisitDateTime,
                ProposedPrice = model.ProposedPrice,
                Images = requestImages
            };

            var result = await requestAppServices.AddNewRequest(newRequest, ct);
            Notification(result);
            if (result.Status == ResultStatusEnum.Warning)
            {
                var categories = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto()
                {
                    Page = 1,
                    PageSize = 100
                }, ct);

                model.CategoryList = categories.Items;

                return View(model);
            }

            if(result.Status == ResultStatusEnum.Failure)
                return RedirectToAction("Index", "Dashboard", new { area = "Customer" });


            var requestSuccessResult = await requestAppServices.GetRequestSuccessInfoByIdAsync(result.Data, ct);

            if (requestSuccessResult is null)
                return RedirectToAction("Index", "Dashboard", new { area = "Customer" });

            var requestSuccessModel = new RequestSuccessViewModel()
            {
                Title = requestSuccessResult.Title,
                WorkTitle = requestSuccessResult.WorkTitle,
                Status = requestSuccessResult.Status,
                Address = requestSuccessResult.Address,
                Date = requestSuccessResult.Date
            };
            return View("RequestSuccess", requestSuccessModel);
        }
    }
}
