using Microsoft.AspNetCore.Mvc;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Endpoint.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        IAuthenticationAppServices authenticationAppServices) : ControllerBase
    {

        [HttpPost("UserRegister")]
        public async Task<Result<string>> Register(UserApiRegisterDto dto, CancellationToken ct)
        {
            return await authenticationAppServices.RegisterAsync(new RegisterDto
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                UserType = dto.UserType
            }, ct);
        }
    }
}
