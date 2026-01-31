using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Controllers
{
    [Area(nameof(Areas.Admin))]
    [Authorize(Roles = "Admin")]
    [LogActivity]
    public class CategoriesController(
        ICategoryAppServices categoryAppServices,
        IMapper mapper) : Controller
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

        public async Task<IActionResult> Index(string? search, int page = 1, CancellationToken ct = default)
        {
            var result = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto
            {
                PageSize = 4,
                TextSearch = search,
                Page = page
            }, ct);

            var model = mapper.Map<CategoriesViewModel>(result);
            model.Search = search;

            return View(model);
        }

        public IActionResult Create()
        {
            return View("CategoryForm", new CategoryFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", model);

            var result = await categoryAppServices.AddAsync(mapper.Map<CategoryDto>(model), ct);
            Notification(result);

            return result.Status == ResultStatusEnum.Success
                ? RedirectToAction(nameof(Index))
                : View("CategoryForm", model);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var result = await categoryAppServices.GetByIdAsync(id, ct);

            Notification(result);
            if (result.Status != ResultStatusEnum.Success || result.Data is null)
                return RedirectToAction("Index", "Categories", new { area = "Admin" });

            var model = mapper.Map<CategoryFormViewModel>(result.Data);
            return View("CategoryForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", model);

            var result = await categoryAppServices.UpdateAsync(mapper.Map<CategoryDto>(model), ct);

            Notification(result);
            return RedirectToAction("Index", "Categories", new { area = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await categoryAppServices.DeleteAsync(id, ct);
            Notification(result);
            return RedirectToAction(nameof(Index));
        }
    }
}
