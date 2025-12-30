using Yarito.Domain.Core.DTOs.Users;

namespace Yarito.Domain.Core.Contracts.Users.Services
{
    public interface IAppUserServices
    {
        Task<AppUserStaticsDto> GetUserCountAsync(CancellationToken ct);
        Task<string> GetNameByIdAsync(int userId, CancellationToken ct);
        Task<bool> AddAsync(RegisterDto dto, CancellationToken ct);
    }
}
