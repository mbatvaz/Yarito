using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Cities.AppServices;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Controllers
{
    [Area(nameof(Areas.Customer))]
    [Authorize(Roles = "Customer")]
    [LogActivity]
    public class ProfileController(
        IAppUserAppServices appUserAppServices,
        ICityAppServices cityAppServices,
        IReviewsAppServices reviewsAppServices,
        IAuthenticationAppServices authenticationAppServices,
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

        public async Task<IActionResult> Form(CancellationToken ct)
        {
            var userResult = await appUserAppServices.GetAppUserFullByIdAsync(GetUserId(), ct);

            if (userResult.Status != ResultStatusEnum.Success || userResult.Data is null)
            {
                Notification(userResult);
                return RedirectToAction("Index", "Dashboard", new { area = "Customer" });
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
        public async Task<IActionResult> Form(CustomerFormViewModel model, CancellationToken ct)
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
                return RedirectToAction("Index", "Dashboard", new { area = "Customer" });

            var citiesPost = await cityAppServices.GetAllAsync(ct);
            model.CityList = citiesPost;
            return View(model);
        }

        public async Task<IActionResult> Expert(int expertId, CancellationToken ct, int page = 1)
        {
            var expertResult = await appUserAppServices.GetAppUserFullByIdAsync(expertId, ct);
            if (expertResult.Status != ResultStatusEnum.Success || expertResult.Data is null || expertResult.Data.UserType != UserTypeEnum.Expert)
                return RedirectToAction("Index", "Dashboard", new { area = "Customer" });

            var expertWorksResult = await appUserAppServices.GetExpertCategoryWorksListDto(expertId, ct);
            var expertReviewsResult = await reviewsAppServices.GetReviewsListAsync(new ReviewReqDto()
            {
                ExpertId = expertId,
                ApprovalStatus = ReviewStatusEnum.Approved,
                PageSize = 6,
                Page = page
            }, ct);

            var model = new ExpertViewModel()
            {
                Info = expertResult.Data,
                Categories = expertWorksResult,
                Reviews = expertReviewsResult.Items,

                Page = page,
                PageSize = expertReviewsResult.PageSize,
                TotalCount = expertReviewsResult.TotalCount
            };
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, CancellationToken ct)
        {
            if(!ModelState.IsValid)
                return View(model);

            var result = await authenticationAppServices.ChangePasswordAsync(new ChangePasswordDto()
            {
                OldPassword = model.OldPassword,
                NewPassword = model.NewPassword,
                Id = GetUserId()
            }, ct);

            Notification(result);
            return result.Status == ResultStatusEnum.Success 
                ? RedirectToAction("Login", "Authentication", new { area = "Account" })
                : View(model);
        }
    }
}
