using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Services.Requests
{
    public class BidServices(
        IBidRepo bidRepo) : IBidServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct) 
            => await bidRepo.GetCountAsync(ct);

        public async Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct)
            => await bidRepo.GetBidsSummaryListAsync(q, ct);
    }
}
