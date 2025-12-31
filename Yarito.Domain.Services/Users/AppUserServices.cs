using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

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

        public async Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct)
            => await appUserRepo.GetAppUserSummaryListAsync(q, ct);

        public async Task<AppUserFullDto?> GetAppUserFullByIdAsync(int userId, CancellationToken ct)
            => await appUserRepo.GetAppUserFullByIdAsync(userId, ct);

        public async Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct)
            => await appUserRepo.GetExpertCategoryWorksListDto(expertId, ct);

        public async Task<bool> SoftDeleteAsync(int userId, CancellationToken ct)
            => await appUserRepo.SoftDeleteAsync(userId, ct);
    }
}
