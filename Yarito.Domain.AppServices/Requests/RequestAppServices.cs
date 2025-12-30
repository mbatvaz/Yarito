using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.AppServices.Requests
{
    public class RequestAppServices(
        IRequestServices requestServices) : IRequestAppServices
    {
        public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct)
            => await requestServices.GetRequestsSummaryListAsync(q, ct);
    }
}
