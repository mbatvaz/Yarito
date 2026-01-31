using Microsoft.AspNetCore.Mvc;
using Yarito.Endpoint.WebApp.MVC.Filters;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Controllers
{
    [Area(nameof(Areas.Expert))]
    [LogActivity]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
