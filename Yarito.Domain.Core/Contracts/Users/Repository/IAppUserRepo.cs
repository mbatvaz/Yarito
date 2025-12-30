using Yarito.Domain.Core.DTOs.Users;

namespace Yarito.Domain.Core.Contracts.Users.Repository
{
    public interface IAppUserRepo
    {
        Task<string?> GetFullNameByIdAsync(int userId, CancellationToken ct);
        Task<bool> AddAsync(RegisterDto dto, CancellationToken ct);
        Task<AppUserStaticsDto?> GetUserCountAsync(CancellationToken ct);
    }
}