using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Domain.Services.Works;

public class WorkServices(IWorkRepo workRepo) : IWorkServices
{
    public async Task<bool> AddAsync(WorkDto newWork, CancellationToken ct) 
        => await workRepo.AddAsync(newWork, ct);

    public async Task<WorkDto?> GetByIdAsync(int workId, CancellationToken ct)
        => await workRepo.GetByIdAsync(workId, ct);

    public async Task<bool> UpdateAsync(WorkDto work, CancellationToken ct) 
        => await workRepo.UpdateAsync(work, ct);

    public async Task<bool> DeleteAsync(int workId, CancellationToken ct)
        => await workRepo.DeleteAsync(workId, ct);
}
