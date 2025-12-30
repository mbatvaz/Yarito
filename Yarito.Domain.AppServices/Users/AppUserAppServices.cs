using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs._Common;

namespace Yarito.Domain.AppServices.Users
{
    public class AppUserAppServices(
        IAppUserServices appUserServices,
        IRequestServices requestServices,
        IBidServices bidServices
        ) : IAppUserAppServices
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
    }
}
