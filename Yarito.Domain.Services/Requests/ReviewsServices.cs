using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Services.Requests
{
    public class ReviewsServices(
        IReviewRepo reviewRepo,
        IRequestServices requestServices,
        IBidServices bidServices) : IReviewsServices
    {
        public async Task<bool> ChangeStatusAsync(int reviewId, ReviewStatusEnum newStatus, CancellationToken ct, bool save = true)
            => await reviewRepo.ChangeStatusAsync(reviewId, newStatus, ct, save);

        public async Task<IReadOnlyList<ReviewSummaryDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewRepo.GetReviewsForHomePageAsync(request, ct);

        public async Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewRepo.GetReviewsListAsync(request, ct);

        public async Task<Result<ReviewSummaryDto>> GetReviewsForRequestByIdAsync(int requestId, CancellationToken ct)
        {
            var result = await reviewRepo.GetReviewsForRequestByIdAsync(requestId, ct);
            return result is null
                ? Result<ReviewSummaryDto>.Failure("نظری برای این درخواست وجود ندارد")
                : Result<ReviewSummaryDto>.Success("نظر مرتبط برای این درخواست یافت شد", result);
        }

        public async Task<Result<bool>> AddAsync(AddNewReviewDto review, CancellationToken ct)
        {
            return await reviewRepo.AddAsync(review, ct)
                ? Result<bool>.Success("نظر شما با موفقیت ثبت شد")
                : Result<bool>.Failure("نظر شما ثبت نشد");
        }

        public async Task<bool> HasReviewForRequestAsync(int requestId, CancellationToken ct)
            => await reviewRepo.HasReviewForRequestAsync(requestId, ct);

        public async Task<Result<BidFullDto>> ReviewsValidationAsync(int requestId, int customerId, CancellationToken ct)
        {
            var requestResult = await requestServices.GetRequestFullByIdAsync(requestId, ct);
            if (requestResult.Status != ResultStatusEnum.Success || requestResult.Data is null)
                return Result<BidFullDto>.Failure(requestResult.Message);

            var request = requestResult.Data;

            if (request.CustomerId != customerId)
                return Result<BidFullDto>.Failure("دسترسی غیرمجاز.");

            if (request.Status != RequestStatusEnum.Completed)
                return Result<BidFullDto>.Warning("این درخواست هنوز تمام نشده است.");


            var bidResult = await bidServices.GetBidFullByIdAsync(request.AcceptedBidId.Value, ct);
            if (bidResult.Status != ResultStatusEnum.Success || bidResult.Data is null)
                return Result<BidFullDto>.Failure(bidResult.Message);

            var bid = bidResult.Data;

            if (bid.RequestId != requestId)
                return Result<BidFullDto>.Failure("پیشنهاد پذیرفته‌شده معتبر نیست.");

            return Result<BidFullDto>.Success("تغییر وضعیت به تکمیل ممکن است", bid);
        }
    }
}
