using Microsoft.AspNetCore.Mvc;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Controllers
{
    [Area(nameof(Areas.Expert))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
