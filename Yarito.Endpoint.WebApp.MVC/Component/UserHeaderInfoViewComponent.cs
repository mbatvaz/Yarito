using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Endpoint.WebApp.MVC.Models;

namespace Yarito.Endpoint.WebApp.MVC.Component
{
    public class UserHeaderInfoViewComponent(
        IAppUserServices userServices,
        IAuthenticationAppServices authenticationAppServices) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ct = HttpContext.RequestAborted;

            try
            {
                var idStr = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

                if (idStr == null)
                    return View("Anonymous");


                var result = await userServices.GetUserHeaderInfoAsync(int.Parse(idStr), ct);

                if (result.Status != ResultStatusEnum.Success || result.Data is null)
                    return View("Anonymous");



                var model = new UserHeaderInfoViewModel
                {
                    FirstName = result.Data.FirstName,
                    LastName = result.Data.LastName,
                    ProfileImagePath = result.Data.ProfileImagePath
                };

                return View("Default", model);
            }
            catch
            {
                return View("Anonymous");
            }
        }
    }
}
