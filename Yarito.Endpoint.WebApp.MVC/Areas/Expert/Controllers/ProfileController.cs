using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Cities.AppServices;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Controllers
{
    [Area(nameof(Areas.Expert))]
    [Authorize(Roles = "Expert")]
    [LogActivity]
    public class ProfileController(
        IAppUserAppServices appUserAppServices,
        ICityAppServices cityAppServices,
        ICategoryAppServices categoryAppServices,
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
                return RedirectToAction("Index", "Dashboard", new { area = "Expert" });
            }

            var cities = await cityAppServices.GetAllAsync(ct);
            var allCategoriesResult = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto { Page = 1, PageSize = 1000 }, ct);
            var allCategories = allCategoriesResult.Items;
            var currentWork = await appUserAppServices.GetExpertCategoryWorksListDto(GetUserId(), ct);

            var model = new ExpertFormViewModel
            {
                CityList = cities,
                CategoryList = allCategories,
                CurrentWorkGrouped = currentWork,
                FirstName = userResult.Data.FirstName,
                LastName = userResult.Data.LastName,
                Email = userResult.Data.Email,
                PhoneNumber = userResult.Data.PhoneNumber,
                CurrentProfileImagePath = userResult.Data.ProfileImgPath,
                CityId = userResult.Data.CityName != null
                    ? cities.FirstOrDefault(c => c?.Name == userResult.Data.CityName)?.Id
                    : null,
                WorkIds = currentWork.SelectMany(c => c.Works.Select(w => w.Id)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Form(ExpertFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var allCategoriesResult = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto { Page = 1, PageSize = 1000 }, ct);
                model.CityList = await cityAppServices.GetAllAsync(ct);
                model.CategoryList = allCategoriesResult.Items;
                model.CurrentWorkGrouped = await appUserAppServices.GetExpertCategoryWorksListDto(GetUserId(), ct);
                return View(model);
            }

            var updateDto = new AppUserUpdateDto
            {
                UserId = GetUserId(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                CityId = model.CityId,
                ProfileImage = model.ProfileImage?.OpenReadStream(),
                ProfileImageFormat = model.ProfileImage is not null ? Path.GetExtension(model.ProfileImage.FileName) : null,
                DeleteProfileImage = model.DeleteProfileImage,
                CurrentProfileImage = model.CurrentProfileImagePath,
                UserType = UserTypeEnum.Expert,
                WorkIds = model.WorkIds
            };

            var result = await appUserAppServices.UpdateAsync(updateDto, ct);
            Notification(result);

            if (result.Status != ResultStatusEnum.Success)
            {
                model.CityList = await cityAppServices.GetAllAsync(ct);
                var allCategoriesResult = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto { Page = 1, PageSize = 1000 }, ct);
                model.CategoryList = allCategoriesResult.Items;
                model.CurrentWorkGrouped = await appUserAppServices.GetExpertCategoryWorksListDto(GetUserId(), ct);
                return View(model);
            }

            return RedirectToAction("Index", "Dashboard", new { area = "Expert" });
        }
    }
}
