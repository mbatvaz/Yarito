using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Services.Requests
{
    public class RequestServices(
        IRequestRepo requestRepo) : IRequestServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct) 
            => await requestRepo.GetCountAsync(ct);

        public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct) 
            => await requestRepo.GetRequestsSummaryListAsync(q, ct);
    }
}
