using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Services.Requests
{
    public class BidServices(
        IBidRepo bidRepo) : IBidServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct) 
            => await bidRepo.GetCountAsync(ct);

        public async Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct)
            => await bidRepo.GetBidsSummaryListAsync(q, ct);

        public async Task<PagedResult<BidFullDto>> GetBidsFullListAsync(BidReqDto q, CancellationToken ct)
            => await bidRepo.GetBidsFullListAsync(q, ct);

        public async Task<Result<BidFullDto>> GetBidFullByIdAsync(int bidId, CancellationToken ct)
        {
            var result = await bidRepo.GetBidFullByIdAsync(bidId, ct);
            return result is not null
                ? Result<BidFullDto>.Success("پیشنهاد مورد نظر یافت شد", result)
                : Result<BidFullDto>.Failure("پیشنهاد مورد نظر یافت نشد");
        }

        public async Task<bool> ChangeSingleStatusAsync(int bidId, BidStatusEnum newStatus, CancellationToken ct)
            => await bidRepo.ChangeSingleStatusAsync(bidId, newStatus, ct);

        public async Task<Result<BidDetailsDto>> GetBidDetailsAsync(int bidId, CancellationToken ct)
        {
            var result = await bidRepo.GetBidDetailsAsync(bidId, ct);
            return result is not null
                ? Result<BidDetailsDto>.Success("پیشنهاد یافت شد", result)
                : Result<BidDetailsDto>.Failure("پیشنهادی مورد نظر یافت نشد");
        }
    }
}
