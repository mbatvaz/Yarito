using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Cities.AppServices;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
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
        ICityAppServices cityAppServices,
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

        public async Task<IActionResult> CustomerForm(CancellationToken ct)
        {
            var userResult = await appUserAppServices.GetAppUserFullByIdAsync(GetUserId(), ct);

            if (userResult.Status != ResultStatusEnum.Success || userResult.Data is null)
            {
                Notification(userResult);
                return RedirectToAction("Index");
            }

            var cities = await cityAppServices.GetAllAsync(ct);

            var model = new CustomerFormViewModel
            {
                CityList = cities,
                FirstName = userResult.Data.FirstName,
                LastName = userResult.Data.LastName,
                Email = userResult.Data.Email,
                Address = userResult.Data.Address,
                PhoneNumber = userResult.Data.PhoneNumber,
                CurrentProfileImagePath = userResult.Data.ProfileImgPath,
                CityId = userResult.Data.CityName != null 
                    ? cities.FirstOrDefault(c => c.Name == userResult.Data.CityName)?.Id 
                    : null
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerForm(CustomerFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var cities = await cityAppServices.GetAllAsync(ct);
                model.CityList = cities;
                return View(model);
            }

            var updateDto = new AppUserUpdateDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                CityId = model.CityId,
                ProfileImage = model.ProfileImage?.OpenReadStream(),
                ProfileImageExtension = model.ProfileImage is not null ? Path.GetExtension(model.ProfileImage.FileName) : null,
                DeleteProfileImage = model.DeleteProfileImage
            };

            var result = await appUserAppServices.UpdateAsync(
                GetUserId(), 
                updateDto, 
                model.CurrentProfileImagePath, 
                ct);

            Notification(result);

            if (result.Status == ResultStatusEnum.Success)
                return RedirectToAction("Index");

            var citiesPost = await cityAppServices.GetAllAsync(ct);
            model.CityList = citiesPost;
            return View(model);
        }
    }
}
