using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با نظرات در لایه نمایش.
/// </summary>
public interface IReviewsAppServices
{
    /// <summary>
    /// تایید یک نظر.
    /// </summary>
    /// <param name="reviewId">شناسه نظر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات</returns>
    Task<Result<bool>> ApproveAsync(int reviewId, CancellationToken ct);

    /// <summary>
    /// رد یک نظر.
    /// </summary>
    /// <param name="reviewId">شناسه نظر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات</returns>
    Task<Result<bool>> RejectAsync(int reviewId, CancellationToken ct);

    /// <summary>
    /// دریافت نظرات تایید شده برای نمایش در صفحه اصلی.
    /// </summary>
    /// <param name="request">پارامترهای جستجو</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست نظرات برای صفحه اصلی</returns>
    Task<IReadOnlyList<ReviewSummaryDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct);

    /// <summary>
    /// دریافت لیست کامل نظرات با قابلیت صفحه‌بندی.
    /// </summary>
    /// <param name="request">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از نظرات</returns>
    Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct);

    Task<Result<ReviewSummaryDto>> GetReviewsForRequestByIdAsync(int requestId, CancellationToken ct);
}
