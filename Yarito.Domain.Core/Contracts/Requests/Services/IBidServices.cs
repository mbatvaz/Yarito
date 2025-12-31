using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با پیشنهادها.
/// </summary>
public interface IBidServices
{
    /// <summary>
    /// دریافت تعداد کل پیشنهادها.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>تعداد کل پیشنهادها</returns>
    Task<int> GetCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه پیشنهادات با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از پیشنهادات</returns>
    Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct);
}
