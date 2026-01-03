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
    public class WorksController(
        ICategoryAppServices categoryAppServices,
        IWorkAppServices workAppServices) : Controller
    {
        private void Notification(Result<string> r) => TempData["Notification"] = JsonConvert.SerializeObject(r);

        public async Task<IActionResult> Create(CancellationToken ct)
        {
            var model = new WorkFormViewModel
            {
                Categories = await categoryAppServices.GetJustCategoriesListAsync(ct)
            };
            return View("WorkForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryAppServices.GetJustCategoriesListAsync(ct);
                return View("WorkForm", model);
            }

            var result = await workAppServices.AddAsync(new WorkDto
            {
                Title = model.Title,
                BasePrice = model.BasePrice,
                CategoryId = model.CategoryId
            }, ct);

            Notification(result);

            if (result.Status == ResultStatusEnum.Success)
                return RedirectToAction("Index", "Categories", new { area = "Admin" });

            model.Categories = await categoryAppServices.GetJustCategoriesListAsync(ct);
            return View("WorkForm", model);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var result = await workAppServices.GetByIdAsync(id, ct);

            if (result.Status != ResultStatusEnum.Success || result.Data is null)
            {
                Notification(Result<string>.Warning(result.Message));
                return RedirectToAction("Index", "Categories", new { area = "Admin" });
            }

            var viewModel = new WorkFormViewModel
            {
                Id = result.Data.Id,
                Title = result.Data.Title,
                BasePrice = result.Data.BasePrice,
                CategoryId = result.Data.CategoryId,
                Categories = await categoryAppServices.GetJustCategoriesListAsync(ct)
            };

            return View("WorkForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkFormViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryAppServices.GetJustCategoriesListAsync(ct);
                return View("WorkForm", model);
            }

            var result = await workAppServices.UpdateAsync(new WorkDto
            {
                Id = model.Id,
                Title = model.Title,
                BasePrice = model.BasePrice,
                CategoryId = model.CategoryId
            }, ct);

            Notification(result);
            return RedirectToAction("Index", "Categories", new { area = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await workAppServices.DeleteAsync(id, ct);
            Notification(result);
            return RedirectToAction("Index", "Categories", new { area = "Admin" });
        }
    }
}
