using Yarito.Domain.Core.DTOs._Common;

namespace Yarito.Domain.Core.Contracts.Users.AppServices
{
    public interface IAppUserAppServices
    {
        Task<AppStatisticsDto> GetStatisticsAsync(CancellationToken ct);
    }
}
