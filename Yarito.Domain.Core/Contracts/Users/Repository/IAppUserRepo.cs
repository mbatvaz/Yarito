using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Users.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی کاربران سیستم.
/// </summary>
public interface IAppUserRepo
{
    /// <summary>
    /// دریافت نام کامل کاربر بر اساس شناسه.
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نام کامل کاربر یا null در صورت عدم یافتن</returns>
    Task<string?> GetFullNameByIdAsync(int userId, CancellationToken ct);

    /// <summary>
    /// ثبت کاربر جدید در دیتابیس.
    /// </summary>
    /// <param name="dto">اطلاعات ثبت‌نام کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(RegisterDto dto, CancellationToken ct);

    /// <summary>
    /// دریافت آمار تعداد کاربران (مشتری و متخصص).
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>آمار تعداد کاربران</returns>
    Task<AppUserStaticsDto?> GetUserCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه کاربران با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از کاربران</returns>
    Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct);

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