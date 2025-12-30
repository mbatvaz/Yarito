using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs._Common;
using Yarito.Domain.Core.DTOs.Users;

namespace Yarito.Domain.Services.Users
{
    public class AppUserServices(
        IAppUserRepo appUserRepo) : IAppUserServices
    {
        public async Task<AppUserStaticsDto> GetUserCountAsync(CancellationToken ct) 
            => await appUserRepo.GetUserCountAsync(ct) 
            ?? new AppUserStaticsDto { NumberOfCustomers = 0, NumberOfExperts = 0};

        public async Task<string> GetNameByIdAsync(int userId, CancellationToken ct) 
            => await appUserRepo.GetFullNameByIdAsync(userId, ct) ?? "ناشناس";

        public async Task<bool> AddAsync(RegisterDto dto, CancellationToken ct) 
            => await appUserRepo.AddAsync(dto, ct);

    }
}
