using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Endpoint.WebApp.MVC.Areas.Account.Models;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Account.Controllers
{
    [Area(nameof(Areas.Account))]
    public class AuthenticationController(
        IAuthenticationAppServices authenticationAppServices) : Controller
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authenticationAppServices.LoginAsync(new LoginDto
            {
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                RememberMe = model.RememberMe
            }, ct);

            Notification(result);

            return result.Data switch
            {
                "Customer" => RedirectToAction("Index", "Dashboard", new { area = "Customer" }),
                "Expert" => RedirectToAction("Index", "Dashboard", new { area = "Expert" }),
                "Admin" => RedirectToAction("Index", "Dashboard", new { area = "Admin" }),
                _ => View(model)
            };
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                PhoneNumber = string.Empty,
                Password = string.Empty,
                UserType = UserTypeEnum.Customer // Default selection
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authenticationAppServices.RegisterAsync(new RegisterDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                UserType = model.UserType
            }, ct);

            Notification(result);
            return result.Status == ResultStatusEnum.Success
                ? RedirectToAction("Index", "Home", new { area = "" })
                : View(model);
        }

        public async Task<IActionResult> Logout()
        {
            var result = await authenticationAppServices.LogoutAsync();
            Notification(result);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
