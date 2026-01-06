using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.AppServices.Requests
{
    public class BidAppServices(
        IBidServices bidServices) : IBidAppServices
    {
        public async Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct)
            => await bidServices.GetBidsSummaryListAsync(q, ct);

        public async Task<Result<bool>> RejectBidAsync(int bidId, CancellationToken ct)
        {
            var result = await bidServices.ChangeSingleStatusAsync(bidId, BidStatusEnum.Rejected, ct);
            return result
                ? Result<bool>.Success("پیشنهاد با موفقیت رد شد.")
                : Result<bool>.Failure("خطا در رد پیشنهاد.");
        }

        public async Task<Result<BidDetailsDto>> GetBidDetailsAsync(int bidId, CancellationToken ct)
            => await bidServices.GetBidDetailsAsync(bidId, ct);
    }
}
