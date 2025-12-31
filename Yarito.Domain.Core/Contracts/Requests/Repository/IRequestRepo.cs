using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی درخواست‌های خدماتی.
/// </summary>
public interface IRequestRepo
{
    /// <summary>
    /// افزودن یک درخواست جدید به دیتابیس.
    /// </summary>
    /// <param name="newRequest">موجودیت درخواست جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(Request newRequest, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک درخواست موجود.
    /// </summary>
    /// <param name="newRequest">موجودیت درخواست با اطلاعات جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(Request newRequest, CancellationToken ct);

    /// <summary>
    /// پذیرش یک پیشنهاد برای درخواست.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="bidId">شناسه پیشنهاد پذیرفته‌شده</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AcceptBidAsync(int requestId, int bidId, CancellationToken ct);

    /// <summary>
    /// تغییر وضعیت یک درخواست.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="newStatus">وضعیت جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct);

    /// <summary>
    /// دریافت تعداد کل درخواست‌ها.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>تعداد کل درخواست‌ها</returns>
    Task<int> GetCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه درخواست‌ها با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از درخواست‌ها</returns>
    Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct);
}
