using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository
{
    public interface IExpertWorkRepo
    {
        Task<IReadOnlyList<WorksFullDto>> GetExpertWorks(int expertId, CancellationToken ct);
    }
}
