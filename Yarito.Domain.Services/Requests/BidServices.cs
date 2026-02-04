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

        public async Task<Result<bool>> RejectAllBidsByRequestIdAsync(int requestId, CancellationToken ct)
        {
            var result = await bidRepo.RejectAllBidsByRequestIdAsync(requestId, ct);
            return result
                ? Result<bool>.Success("تمامی پیشنهادهای مربوط به این درخواست لغو شدند.")
                : Result<bool>.Failure("خطا در لغو پیشنهادهای مربوط به درخواست.");
        }

        public async Task<Result<bool>> RejectBidAsync(int bidId, CancellationToken ct)
        {
            var updateResult = await bidRepo.ChangeSingleStatusAsync(bidId, BidStatusEnum.Rejected, ct);
            return updateResult
                ? Result<bool>.Success("پیشنهاد با موفقیت رد شد.")
                : Result<bool>.Failure("خطا در رد پیشنهاد.");
        }

        public async Task<Result<bool>> AcceptBidAsync(int bidId, int requestId, CancellationToken ct)
        {
            return await bidRepo.AcceptBidAndRejectOthersAsync(bidId, requestId, ct)
                ? Result<bool>.Success("پیشنهاد با موفقیت پذیرفته شد.")
                : Result<bool>.Failure("خطا در پذیرش پیشنهاد.");
        }
    }
}
