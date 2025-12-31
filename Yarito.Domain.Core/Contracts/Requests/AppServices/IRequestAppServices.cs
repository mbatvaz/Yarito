using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با درخواست‌ها در لایه نمایش.
/// </summary>
public interface IRequestAppServices
{
    /// <summary>
    /// دریافت لیست خلاصه درخواست‌ها با قابلیت صفحه‌بندی.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از درخواست‌ها</returns>
    Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct);
}
