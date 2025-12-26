using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Contracts.Users.Repository
{
    public interface IExpertRepo
    {
        Task<bool> AddAsync(Expert newExpert, CancellationToken ct);
        Task<bool> UpdateAsync(Expert newExpert, CancellationToken ct);
        Task<bool> DeleteAsync(int expertId, CancellationToken ct);
    }
}
