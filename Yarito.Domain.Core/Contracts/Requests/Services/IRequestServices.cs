using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با درخواست‌ها.
/// </summary>
public interface IRequestServices
{
    #region Query Methods

    /// <summary>
    /// دریافت تعداد کل درخواست‌ها.
    /// </summary>
    Task<int> GetCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت جزئیات کامل یک درخواست با شناسه.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات کامل درخواست یا null در صورت عدم وجود</returns>
    Task<RequestFullDto?> GetRequestFullByIdAsync(int requestId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه درخواست‌ها با قابلیت صفحه‌بندی.
    /// </summary>
    Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت لیست کارت‌های درخواست با قابلیت صفحه‌بندی.
    /// </summary>
    Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct);

    #endregion

    #region Command Methods

    /// <summary>
    /// تغییر وضعیت یک درخواست.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="newStatus">وضعیت جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct);

    #endregion
}
