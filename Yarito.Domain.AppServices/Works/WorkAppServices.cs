using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;

namespace Yarito.Domain.AppServices.Works;

public class WorkAppServices(IWorkServices workServices) : IWorkAppServices
{
    public async Task<Result<WorkDto>> GetByIdAsync(int workId, CancellationToken ct)
    {
        return await workServices.GetByIdAsync(workId, ct);
    }

    public async Task<Result<WorkDto>> AddAsync(WorkDto newWork, CancellationToken ct)
    {
        var validationResults = workServices.IsPropertyValid(newWork);
        if (validationResults.Status != ResultStatusEnum.Success || validationResults.Data is null)
            return validationResults;

        var duplicationResult = await workServices.IsTitleDuplicationAsync(newWork.Title, ct);
        if (duplicationResult.Status != ResultStatusEnum.Success)
            return duplicationResult;

        return await workServices.AddAsync(validationResults.Data, ct);
    }

    public async Task<Result<WorkDto>> UpdateAsync(WorkDto work, CancellationToken ct)
    {
        var validationResults = workServices.IsPropertyValid(work);
        if (validationResults.Status != ResultStatusEnum.Success || validationResults.Data is null)
            return validationResults;

        var duplicationResult = await workServices.IsTitleDuplicationAsync(work.Title, ct, work.Id);
        if (duplicationResult.Status != ResultStatusEnum.Success)
            return duplicationResult;

        return await workServices.UpdateAsync(validationResults.Data, ct);
    }

    public async Task<Result<bool>> DeleteAsync(int workId, CancellationToken ct)
    {
        return await workServices.DeleteAsync(workId, ct);
    }
}
