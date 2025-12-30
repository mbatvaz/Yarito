using Microsoft.AspNetCore.Mvc;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Controllers
{
    [Area(nameof(Areas.Customer))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
