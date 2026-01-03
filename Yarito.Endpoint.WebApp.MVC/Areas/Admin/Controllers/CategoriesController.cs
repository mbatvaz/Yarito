using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Controllers
{
    [Area(nameof(Areas.Admin))]
    public class CategoriesController(
        ICategoryAppServices categoryAppServices) : Controller
    {
        private void Notification(Result<string> r) => TempData["Notification"] = JsonConvert.SerializeObject(r);

        public async Task<IActionResult> Index(
            string? search, int page = 1, CancellationToken ct = default)
        {
            var result = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto
            {
                PageSize = 4,
                TextSearch = search,
                Page = page
            }, ct);

            var viewModel = new CategoriesViewModel
            {
                Categories = result.Items,
                Search = search,
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / result.PageSize)
            };

            return View(viewModel);
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

            var result = await categoryAppServices.AddAsync(new CategoryDto
            {
                Title = model.Title,
                Description = model.Description
            }, ct);

            Notification(result);

            return result.Status == ResultStatusEnum.Success
                ? RedirectToAction("Index", "Categories", new { area = "Admin" })
                : View("CategoryForm", model);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var result = await categoryAppServices.GetByIdAsync(id, ct);

            if (result.Status != ResultStatusEnum.Success || result.Data is null)
            {
                Notification(Result<string>.Warning(result.Message));
                return RedirectToAction("Index", "Categories", new { area = "Admin" });
            }

            var viewModel = new CategoryFormViewModel
            {
                Id = result.Data.Id,
                Title = result.Data.Title,
                Description = result.Data.Description
            };

            return View("CategoryForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", model);

            var result = await categoryAppServices.UpdateAsync(new CategoryDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description
            }, ct);

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
