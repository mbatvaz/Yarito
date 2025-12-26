using Yarito.Domain.Core.Entities.Cities;

namespace Yarito.Domain.Core.Contracts.Cities.Repository
{
    public interface ICityRepo
    {
        Task<bool> AddAsync(City newCity, CancellationToken ct);
        Task<bool> UpdateAsync(City newCity, CancellationToken ct);
        Task<bool> DeleteAsync(int cityId, CancellationToken ct);
    }
}
