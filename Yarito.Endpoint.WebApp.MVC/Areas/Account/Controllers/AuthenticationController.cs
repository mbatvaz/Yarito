using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Account.Controllers
{
    [Area(nameof(Areas.Account))]
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            var obj = Result<string>.Success("این یه پیام تست است");
            TempData["Notification"] = JsonConvert.SerializeObject(obj);
            return View();
        }

        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
