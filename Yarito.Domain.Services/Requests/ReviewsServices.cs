using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Services.Requests
{
    public class ReviewsServices(
        IReviewRepo reviewRepo) : IReviewsServices
    {
        public async Task<bool> ChangeStatusAsync(int reviewId, ReviewStatusEnum newStatus, CancellationToken ct)
            => await reviewRepo.ChangeStatusAsync(reviewId, newStatus, ct);

        public async Task<IReadOnlyList<ReviewSummaryDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewRepo.GetReviewsForHomePageAsync(request, ct);

        public async Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewRepo.GetReviewsListAsync(request, ct);

        public async Task<Result<ReviewSummaryDto>> GetReviewsForRequestByIdAsync(int requestId, CancellationToken ct)
        {
            var result = await reviewRepo.GetReviewsForRequestByIdAsync(requestId, ct);
            return result is null
                ? Result<ReviewSummaryDto>.Failure("نظری برای این درخواست مجو ندارد")
                : Result<ReviewSummaryDto>.Success("نظر مرتبط برای این درخواست یافت شد", result);
        }
    }
}
