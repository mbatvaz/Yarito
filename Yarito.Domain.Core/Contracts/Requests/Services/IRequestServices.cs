using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با درخواست‌ها.
/// </summary>
public interface IRequestServices
{
    /// <summary>
    /// دریافت تعداد کل درخواست‌ها.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>تعداد کل درخواست‌ها</returns>
    Task<int> GetCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه درخواست‌ها با قابلیت صفحه‌بندی.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از درخواست‌ها</returns>
    Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct);
}
