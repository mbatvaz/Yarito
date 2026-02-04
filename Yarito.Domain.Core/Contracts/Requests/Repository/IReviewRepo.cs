using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی نظرات.
/// </summary>
public interface IReviewRepo
{
    /// <summary>
    /// افزودن یک نظر جدید به دیتابیس.
    /// </summary>
    /// <param name="newReview">موجودیت نظر جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(AddNewReviewDto newReview, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک نظر موجود.
    /// </summary>
    /// <param name="newReview">موجودیت نظر با اطلاعات جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(Review newReview, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک نظر از دیتابیس.
    /// </summary>
    /// <param name="reviewId">شناسه نظر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(int reviewId, CancellationToken ct);

    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> ChangeStatusAsync(int reviewId, ReviewStatusEnum newStatus, CancellationToken ct, bool save);

    /// <summary>
    /// دریافت نظرات تایید شده برای نمایش در صفحه اصلی.
    /// </summary>
    /// <param name="q">پارامترهای جستجو</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست نظرات برای نمایش در صفحه اصلی</returns>
    Task<IReadOnlyList<ReviewSummaryDto>> GetReviewsForHomePageAsync(ReviewReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت لیست کامل نظرات با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از نظرات</returns>
    Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto q, CancellationToken ct);


    Task<ReviewSummaryDto?> GetReviewsForRequestByIdAsync(int requestId, CancellationToken ct);
    Task<bool> HasReviewForRequestAsync(int requestId, CancellationToken ct);
}
