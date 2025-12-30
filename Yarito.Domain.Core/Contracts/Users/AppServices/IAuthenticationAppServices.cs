using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Users.AppServices
{
    public interface IAuthenticationAppServices
    {
        Task<Result<string>> LoginAsync(LoginDto dto, CancellationToken ct);
        Task<Result<string>> LogoutAsync();
        Task<Result<string>> RegisterAsync(RegisterDto dto, CancellationToken ct);
    }
}
