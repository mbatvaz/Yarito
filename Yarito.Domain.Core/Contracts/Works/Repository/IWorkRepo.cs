using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository
{
    public interface IWorkRepo
    {
        Task<bool> AddAsync(Work newWork, CancellationToken ct);
        Task<bool> UpdateAsync(Work newWork, CancellationToken ct);
        Task<bool> DeleteAsync(int workId, CancellationToken ct);
    }
}
