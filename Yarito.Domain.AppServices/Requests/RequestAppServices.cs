using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.AppServices.Requests
{
    public class RequestAppServices(
        IRequestServices requestServices) : IRequestAppServices
    {
        #region Query Methods

        public async Task<RequestFullDto?> GetRequestFullByIdAsync(int requestId, CancellationToken ct)
            => await requestServices.GetRequestFullByIdAsync(requestId, ct);

        public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct)
            => await requestServices.GetRequestsSummaryListAsync(q, ct);

        public async Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct)
            => await requestServices.GetRequestsCardListAsync(q, ct);

        #endregion

        #region Command Methods

        public async Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct)
            => await requestServices.ChangeStatusAsync(requestId, newStatus, ct);

        #endregion
    }
}
