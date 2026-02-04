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
    }
}
