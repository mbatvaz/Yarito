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
    #region Query Methods

    /// <summary>
    /// دریافت تعداد کل درخواست‌ها.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>تعداد کل درخواست‌ها</returns>
    Task<int> GetCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت جزئیات کامل یک درخواست با شناسه.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات کامل درخواست یا null در صورت عدم وجود</returns>
    Task<RequestFullDto?> GetRequestFullByIdAsync(int requestId, CancellationToken ct);

    /// <summary>
    /// اطلاعات مخصوص ثبت موفق یک درخواست را باز میگرداند
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>درصورت وجود اطلاعات لازم در غیر این صورت null</returns>
    Task<RequestSuccessDto?> GetRequestSuccessInfoByIdAsync(int requestId, CancellationToken ct);
    /// <summary>
    /// دریافت لیست خلاصه درخواست‌ها با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از درخواست‌ها</returns>
    Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت لیست کارت‌های درخواست با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از کارت‌های درخواست</returns>
    Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct);

    #endregion

    #region Command Methods

    /// <summary>
    /// افزودن یک درخواست جدید به دیتابیس.
    /// </summary>
    /// <param name="dto">موجودیت درخواست جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>بازگرداند ایدی درخواست در صورت موفقیت</returns>
    Task<int> AddAsync(RequestNewDto dto, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک درخواست موجود.
    /// </summary>
    /// <param name="newRequest">موجودیت درخواست با اطلاعات جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(Request newRequest, CancellationToken ct);

    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AcceptBidAsync(int requestId, int bidId, CancellationToken ct, bool save);

    /// <summary>
    /// تغییر وضعیت یک درخواست.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="newStatus">وضعیت جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct, bool save);

    /// <summary>
    /// ذخیره تغییرات اعمال شده در دیتابیس.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت عملیات ذخیره</returns>
    Task<bool> SaveChangesAsync(CancellationToken ct);

    #endregion


    Task<int> CountOfOpenRequestForCustomerIdAsync(int customerId, CancellationToken ct);
    void ClearChangeTracker();

    /// <summary>
    /// دریافت لیست مراجعات یک متخصص بر اساس فیلترهای مشخص شده (مانند تاریخ امروز).
    /// </summary>
    //Task<PagedResult<ExpertDashboardVisitDto>> GetExpertVisitsAsync(RequestReqDto q, CancellationToken ct);
}
