using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.AppServices.Requests;

public class ReviewsAppServices(
    IReviewsServices reviewsServices,
    IRequestServices requestServices) : IReviewsAppServices
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

    public async Task<IReadOnlyList<ReviewSummaryDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct)
        => await reviewsServices.GetReviewsForHomePageAsync(request, ct);

    public async Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct)
        => await reviewsServices.GetReviewsListAsync(request, ct);

    public async Task<Result<ReviewSummaryDto>> GetReviewsForRequestByIdAsync(int requestId, CancellationToken ct)
        => await reviewsServices.GetReviewsForRequestByIdAsync(requestId, ct);

    public async Task<Result<bool>> RegisterReviewAsync(AddNewReviewDto dto, CancellationToken ct)
    {
        var validationResult = await reviewsServices.ReviewsValidationAsync(dto.RequestId, dto.CustomerId, ct);
        if (validationResult.Status != ResultStatusEnum.Success)
            return Result<bool>.Warning(validationResult.Message);

        if (await reviewsServices.HasReviewForRequestAsync(dto.RequestId, ct))
            return Result<bool>.Failure("شما قبلاً برای این درخواست نظر ثبت کرده‌اید.");

        var bid = validationResult.Data;

        dto.ExpertId = bid.ExpertId;

        var addResult = await reviewsServices.AddAsync(dto, ct);
        return addResult.Status == ResultStatusEnum.Success
            ? Result<bool>.Success("نظر شما با موفقیت ثبت شد و پس از تایید نمایش داده خواهد شد.")
            : Result<bool>.Failure("خطا در ثبت نظر.");
    }
}
