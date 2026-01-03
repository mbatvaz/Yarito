using Yarito.Domain.Core.Contracts.Cities.AppServices;
using Yarito.Domain.Core.Contracts.Cities.Services;
using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.AppServices.Cities
{
    public class CityAppServices(
        ICityServices cityServices) : ICityAppServices
    {
        public async Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct)
            => await cityServices.GetAllAsync(ct);
    }
}
