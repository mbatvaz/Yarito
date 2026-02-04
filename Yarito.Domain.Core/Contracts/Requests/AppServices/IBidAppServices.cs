using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با پیشنهادات در لایه نمایش.
/// </summary>
public interface IBidAppServices
{
    /// <summary>
    /// دریافت لیست خلاصه پیشنهادات با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از پیشنهادات</returns>
    Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت لیست اطلاعات کامل پیشنهادات با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از پیشنهادات</returns>
    Task<PagedResult<BidFullDto>> GetBidsFullListAsync(BidReqDto q, CancellationToken ct);

    /// <summary>
    /// رد کردن یک پیشنهاد با بررسی صحت تعلق به درخواست و مالکیت اختیاری.
    /// </summary>
    /// <param name="bidId">شناسه پیشنهاد</param>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="userId">شناسه کاربر (مشتری - برای بررسی دسترس)</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات</returns>
    Task<Result<bool>> RejectBidAsync(int bidId, int requestId, int? userId, CancellationToken ct);

    /// <summary>
    /// دریافت جزئیات کامل یک پیشنهاد شامل اطلاعات مشتری، متخصص و درخواست.
    /// </summary>
    /// <param name="bidId">شناسه پیشنهاد</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات کامل پیشنهاد یا null در صورت عدم وجود</returns>
    Task<Result<BidDetailsDto>> GetBidDetailsAsync(int bidId, CancellationToken ct);
}
