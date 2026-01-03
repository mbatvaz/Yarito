using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.Core.Contracts.Cities.Services
{
    public interface ICityServices
    {
        Task<bool> IsExistAsync(int cityId, CancellationToken ct);

        Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct);
    }
}
