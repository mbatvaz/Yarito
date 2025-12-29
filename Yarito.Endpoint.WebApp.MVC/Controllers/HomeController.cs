using Microsoft.AspNetCore.Mvc;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Endpoint.WebApp.MVC.Models;

namespace Yarito.Endpoint.WebApp.MVC.Controllers
{
    public class HomeController(
        IReviewsAppServices reviewsAppServices,
        ICategoryAppServices categoryAppServices) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var model = new HomeViewModel()
            {
                Reviews = await reviewsAppServices.GetReviewsForHomePageAsync(new ReviewReqDto() 
                {
                    ApprovalStatus = ReviewStatusEnum.Approved,
                    MinRating = 4,
                    PageSize = 6

                }, ct),
                Categories = await categoryAppServices.GetAllCategoriesNamesAsync(ct)
            };

            return View(model);
        }
    }
}
