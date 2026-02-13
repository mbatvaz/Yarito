using Yarito.Domain.Core.Contracts.Cities.Repository;
using Yarito.Domain.Core.Contracts.Cities.Services;
using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.Services.Cities
{
    public class CityServices(
        ICityRepo cityRepo,
        ICityQueryRepo cityQueryRepo) : ICityServices
    {
        public async Task<bool> IsExistAsync(int cityId, CancellationToken ct)
            => await cityRepo.IsExistAsync(cityId, ct);

        public async Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct)
            => await cityQueryRepo.GetAllAsync(ct);
    }
}
