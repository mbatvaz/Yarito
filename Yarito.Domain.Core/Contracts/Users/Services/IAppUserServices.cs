using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Users.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با کاربران.
/// </summary>
public interface IAppUserServices
{
    /// <summary>
    /// دریافت آمار تعداد کاربران سیستم.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>آمار تعداد مشتریان و متخصصان</returns>
    Task<AppUserStaticsDto> GetUserCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت نام کاربر بر اساس شناسه.
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نام کاربر</returns>
    Task<string> GetNameByIdAsync(int userId, CancellationToken ct);

    /// <summary>
    /// ثبت کاربر جدید در سیستم.
    /// </summary>
    /// <param name="dto">اطلاعات ثبت‌نام کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(RegisterDto dto, CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه کاربران با قابلیت صفحه‌بندی.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از کاربران</returns>
    Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت اطلاعات کامل کاربر بر اساس شناسه کاربر.
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات کامل کاربر</returns>
    Task<AppUserFullDto?> GetAppUserFullByIdAsync(int userId, CancellationToken ct);

    Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct);

    /// <summary>
    /// حذف نرم کاربر بر اساس شناسه.
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> SoftDeleteAsync(int userId, CancellationToken ct);
}
