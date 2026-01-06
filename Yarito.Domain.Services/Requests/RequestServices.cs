using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Services.Requests
{
    public class RequestServices(
        IRequestRepo requestRepo) : IRequestServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct)
            => await requestRepo.GetCountAsync(ct);

        public async Task<RequestFullDto?> GetRequestFullByIdAsync(int requestId, CancellationToken ct)
            => await requestRepo.GetRequestFullByIdAsync(requestId, ct);

        public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q,
            CancellationToken ct)
            => await requestRepo.GetRequestsSummaryListAsync(q, ct);

        public async Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct)
            => await requestRepo.GetRequestsCardListAsync(q, ct);

        public async Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus,
            CancellationToken ct)
        {
            return await requestRepo.ChangeStatusAsync(requestId, newStatus, ct)
                ? Result<bool>.Success("وضعیت درخواست با موفقیت تغییر کرد.")
                : Result<bool>.Failure("خطا در تغییر وضعیت درخواست.");
        }
    }
}
