using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با درخواست‌ها در لایه نمایش.
/// </summary>
public interface IRequestAppServices
{
    #region Query Methods

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
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از درخواست‌ها</returns>
    Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت لیست کارت‌های درخواست با قابلیت صفحه‌بندی.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از کارت‌های درخواست</returns>
    Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct);

    #endregion

    #region Command Methods

    /// <summary>
    /// تغییر وضعیت یک درخواست.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="newStatus">وضعیت جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات</returns>
    Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct);

    #endregion
}
