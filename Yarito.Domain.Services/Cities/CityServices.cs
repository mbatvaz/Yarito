using Yarito.Domain.Core.Contracts.Cities.Repository;
using Yarito.Domain.Core.Contracts.Cities.Services;
using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.Services.Cities
{
    public class CityServices(
        ICityRepo cityRepo) : ICityServices
    {
        public async Task<bool> IsExistAsync(int cityId, CancellationToken ct)
            => await cityRepo.IsExistAsync(cityId, ct);

        public async Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct)
            => await cityRepo.GetAllAsync(ct);
    }
}
