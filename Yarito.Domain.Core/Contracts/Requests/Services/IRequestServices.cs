using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Works;
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

    public void ClearChangeTracker();

    /// <summary>
    /// دریافت جزئیات کامل یک درخواست با شناسه.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات کامل درخواست یا null در صورت عدم وجود</returns>
    Task<Result<RequestFullDto>> GetRequestFullByIdAsync(int requestId, CancellationToken ct);

    /// <summary>
    /// دریافت اطلاعات لازم پس از ثبت موفق یک درخواست
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات لازم درخواست یا null در صورت عدم وجود</returns>
    Task<Result<RequestSuccessDto>> GetRequestSuccessInfoByIdAsync(int requestId, CancellationToken ct);
    
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
    Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct, bool save = true);
    
    Task<bool> SaveChangesAsync(CancellationToken ct);

    #endregion

    /// <summary>
    /// تمام پراپرتی های یک درخواست جدید را صحت سنجی و نرمال سازی میکند
    /// </summary>
    /// <param name="dto">dto درخواست جدید</param>
    /// <returns>نتیجه فرایند صحت سنجی dto و حالت نرمال شده</returns>
    Result<RequestNewDto> IsPropertyValid(RequestNewDto dto);

    /// <summary>
    /// بررسی تعداد درخواست ثبت شده برای مشتری
    /// در صورتی که کاربر بیش از حد نصاب درخواست ثبت کند هشدار میدهد
    /// </summary>
    /// <param name="customerId">شناسه یکتای مشتری</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه اعتبار سنجی</returns>
    Task<Result<bool>> CountOfOpenRequestForCustomerIdAsync(int customerId, CancellationToken ct);

    /// <summary>
    /// بررسی مجاز بودن قیمت پیشنهادی برای خدمات توسط کاربر
    /// کاربر فقط در محدوده ای مشخص از قیمت پایه میتواند درخواست ثبت کند
    /// </summary>
    /// <param name="proposedPrice">قیمت پیشنهادی</param>
    /// <param name="work">اطلاعات خدمات انتخابی کاربر</param>
    /// <returns>مشتری قیمت مجازی پیشنهاد داده است یا خیر</returns>
    Result<bool> IsProposedPriceAllow(decimal proposedPrice, WorkDto work);

    Task<Result<int>> Add(RequestNewDto dto, CancellationToken ct);

    Task<Result<BidFullDto>> CompletionValidationAsync(int requestId, int customerId, CancellationToken ct);
}
