using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.AppServices.Requests
{
    public class BidAppServices(
        IBidServices bidServices) : IBidAppServices
    {
        public async Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct)
            => await bidServices.GetBidsSummaryListAsync(q, ct);
    }
}
