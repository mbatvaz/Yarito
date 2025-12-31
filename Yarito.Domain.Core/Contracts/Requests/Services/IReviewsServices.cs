using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با نظرات.
/// </summary>
public interface IReviewsServices
{
    /// <summary>
    /// دریافت نظرات تایید شده برای نمایش در صفحه اصلی.
    /// </summary>
    /// <param name="request">پارامترهای جستجو</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست نظرات برای صفحه اصلی</returns>
    Task<IReadOnlyList<HomeViewReviewDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct);

    /// <summary>
    /// دریافت لیست کامل نظرات با قابلیت صفحه‌بندی.
    /// </summary>
    /// <param name="request">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از نظرات</returns>
    Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct);
}