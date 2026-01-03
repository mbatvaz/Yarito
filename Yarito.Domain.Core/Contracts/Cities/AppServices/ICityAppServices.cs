using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.Core.Contracts.Cities.AppServices
{
    public interface ICityAppServices
    {
        Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct);
    }
}
