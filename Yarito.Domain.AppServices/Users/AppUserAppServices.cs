using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs._Common;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.AppServices.Users
{
    public class AppUserAppServices(
        IAppUserServices appUserServices,
        IRequestServices requestServices,
        IBidServices bidServices) : IAppUserAppServices
    {
        public async Task<AppStatisticsDto> GetStatisticsAsync(CancellationToken ct)
        {
            var userStatistics = await appUserServices.GetUserCountAsync(ct);
            return new AppStatisticsDto
            {
                NumberOfCustomers = userStatistics.NumberOfCustomers,
                NumberOfExperts = userStatistics.NumberOfExperts,
                NumberOfRequests = await requestServices.GetCountAsync(ct),
                NumberOfBid = await bidServices.GetCountAsync(ct),
            };
        }

        public async Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q,
            CancellationToken ct)
            => await appUserServices.GetAppUserSummaryListAsync(q, ct);

        public async Task<Result<AppUserFullDto>> GetAppUserFullByIdAsync(int userId, CancellationToken ct)
        { 
            var user = await appUserServices.GetAppUserFullByIdAsync(userId, ct);
            return user is null 
                ? Result<AppUserFullDto>.Warning("کاربر یافت نشد")
                : Result<AppUserFullDto>.Success("کاربر مورد نظر یافت شد", user);
        }

        public async Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct)
            => await appUserServices.GetExpertCategoryWorksListDto(expertId, ct);

        public async Task<Result<string>> SoftDeleteAsync(int userId, CancellationToken ct)
        {
            var result = await appUserServices.SoftDeleteAsync(userId, ct);
            return result
                ? Result<string>.Success("کاربر با موفقیت حذف شد")
                : Result<string>.Failure("خطا در حذف کاربر");
        }
    }
}
