using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.AppServices.Requests;

public class ReviewsAppServices(IReviewsServices reviewsServices) : IReviewsAppServices
{
    public async Task<Result<bool>> ApproveAsync(int reviewId, CancellationToken ct)
    {
        var result = await reviewsServices.ChangeStatusAsync(reviewId, ReviewStatusEnum.Approved, ct);
        return result
            ? Result<bool>.Success("نظر با موفقیت تایید شد.")
            : Result<bool>.Failure("خطا در تایید نظر.");
    }

    public async Task<Result<bool>> RejectAsync(int reviewId, CancellationToken ct)
    {
        var result = await reviewsServices.ChangeStatusAsync(reviewId, ReviewStatusEnum.Rejected, ct);
        return result
            ? Result<bool>.Success("نظر با موفقیت رد شد.")
            : Result<bool>.Failure("خطا در رد نظر.");
    }

    public async Task<IReadOnlyList<HomeViewReviewDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct)
        => await reviewsServices.GetReviewsForHomePageAsync(request, ct);

    public async Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct)
        => await reviewsServices.GetReviewsListAsync(request, ct);
}
