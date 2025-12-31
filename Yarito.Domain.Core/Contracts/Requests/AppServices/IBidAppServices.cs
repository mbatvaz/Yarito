using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.AppServices
{
    public interface IBidAppServices
    {
        Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct);
    }
}
