using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;

namespace Yarito.Domain.AppServices.Requests
{
    public class BidAppServices(
        IBidServices bidServices,
        IRequestServices requestServices) : IBidAppServices
    {
        public async Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct)
            => await bidServices.GetBidsSummaryListAsync(q, ct);

        public async Task<PagedResult<BidFullDto>> GetBidsFullListAsync(BidReqDto q, CancellationToken ct)
            => await bidServices.GetBidsFullListAsync(q, ct);

        public async Task<Result<bool>> RejectBidAsync(int bidId, int requestId, int? userId, CancellationToken ct)
        {
            var bidResult = await bidServices.GetBidFullByIdAsync(bidId, ct);
            if (bidResult.Status != ResultStatusEnum.Success || bidResult.Data is null)
                return Result<bool>.Failure("پیشنهاد مورد نظر یافت نشد.");

            if (bidResult.Data.RequestId != requestId)
                return Result<bool>.Failure("این پیشنهاد مربوط به این درخواست نمی‌باشد.");

            if (userId.HasValue)
            {
                var requestResult = await requestServices.GetRequestFullByIdAsync(requestId, ct);
                if (requestResult.Status != ResultStatusEnum.Success || requestResult.Data is null || requestResult.Data.CustomerId != userId.Value)
                    return Result<bool>.Failure("دسترسی غیرمجاز.");
            }

            return await bidServices.RejectBidAsync(bidId, ct);
        }

        public async Task<Result<BidDetailsDto>> GetBidDetailsAsync(int bidId, CancellationToken ct)
            => await bidServices.GetBidDetailsAsync(bidId, ct);

        public async Task<PagedResult<BidForRequestDto>> GetExpertBids(BidReqDto q, CancellationToken ct)
            => await bidServices.GetExpertBids(q, ct);

        public async Task<Result<BidFullDto>> GetExpertBidForRequestAsync(int requestId, int expertId,
            CancellationToken ct) => await bidServices.GetExpertBidForRequestAsync(requestId, expertId, ct);

        public async Task<Result<bool>> AddNewBidAsync(AddNewBidDto dto, CancellationToken ct)
        {
            var result = await bidServices.AddValidateAsync(dto, ct);

            if (result.Status == ResultStatusEnum.Failure || result.Data is null)
                return Result<bool>.Failure(result.Message);

            if (result.Status == ResultStatusEnum.Warning || result.Data is null)
                return Result<bool>.Warning(result.Message);

            return await bidServices.AddAsync(result.Data, ct);
        }

        public async Task<Result<bool>> DeleteAsync(int requestId, int bidId, int expertId, CancellationToken ct)
        {
            var result = await bidServices.DeleteValidateAsync(requestId, bidId, expertId, ct);

            if (result.Status == ResultStatusEnum.Failure)
                return Result<bool>.Failure(result.Message);
            if (result.Status == ResultStatusEnum.Warning)
                return Result<bool>.Warning(result.Message);

            return await bidServices.DeleteAsync(bidId, ct);
        }
    }
}
