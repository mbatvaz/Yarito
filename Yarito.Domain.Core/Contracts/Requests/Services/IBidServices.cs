using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

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

    /// <summary>
    /// دریافت لیست اطلاعات کامل پیشنهادات با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از پیشنهادات</returns>
    Task<PagedResult<BidFullDto>> GetBidsFullListAsync(BidReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت  اطلاعات کامل پیشنهاد بر اساس شناسه.
    /// </summary>
    /// <param name="bidId">شناسه پیشنهاد</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه اطلاعات کامل پیشنهاد</returns>
    Task<Result<BidFullDto>> GetBidFullByIdAsync(int bidId, CancellationToken ct);

    /// <summary>
    /// افزودن یک پیشنهاد جدید.
    /// </summary>
    Task<Result<bool>> AddAsync(AddNewBidDto dto, CancellationToken ct);

    Task<Result<AddNewBidDto>> AddValidateAsync(AddNewBidDto dto, CancellationToken ct);

    /// <summary>
    /// تغییر وضعیت یک پیشنهاد.
    /// </summary>
    /// <param name="bidId">شناسه پیشنهاد</param>
    /// <param name="newStatus">وضعیت جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<Result<bool>> ChangeSingleStatusAsync(int bidId, BidStatusEnum newStatus, CancellationToken ct, bool save = true);

    /// <summary>
    /// دریافت جزئیات کامل یک پیشنهاد شامل اطلاعات مشتری، متخصص و درخواست.
    /// </summary>
    /// <param name="bidId">شناسه پیشنهاد</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات کامل پیشنهاد یا null در صورت عدم وجود</returns>
    Task<Result<BidDetailsDto>> GetBidDetailsAsync(int bidId, CancellationToken ct);

    /// <summary>
    /// رد کردن یک پیشنهاد (تغییر وضعیت به Rejected).
    /// </summary>
    /// <param name="bidId">شناسه پیشنهاد</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات</returns>
    Task<Result<bool>> RejectBidAsync(int bidId, CancellationToken ct, bool save = true);

    /// <summary>
    /// پذیرش یک پیشنهاد و رد بقیه.
    /// </summary>
    Task<Result<bool>> AcceptBidAsync(int bidId, int requestId, CancellationToken ct, bool save = true);

    /// <summary>
    /// رد کردن تمامی پیشنهادهای یک درخواست به صورت انبوه.
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه لغو تمام پیشنهادات</returns>
    Task<Result<bool>> RejectAllBidsByRequestIdAsync(int requestId, CancellationToken ct, bool save = true);

    Task<PagedResult<BidForRequestDto>> GetExpertBids(BidReqDto q, CancellationToken ct);

    Task<Result<BidFullDto>> GetExpertBidForRequestAsync(int requestId, int expertId, CancellationToken ct);

    Task<Result<bool>> DeleteValidateAsync(int requestId, int bidId, int expertId, CancellationToken ct);

    Task<Result<bool>> DeleteAsync(int bidId, CancellationToken ct);
}
