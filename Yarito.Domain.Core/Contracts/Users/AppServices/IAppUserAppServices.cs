using Yarito.Domain.Core.DTOs._Common;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Users.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با کاربران در لایه نمایش.
/// </summary>
public interface IAppUserAppServices
{
    /// <summary>
    /// دریافت آمار کلی سیستم شامل تعداد کاربران، درخواست‌ها و پیشنهادها.
    /// </summary>
    Task<AppStatisticsDto> GetStatisticsAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه کاربران با قابلیت صفحه‌بندی.
    /// </summary>
    Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct);

    Task<Result<AppUserSummaryDto>> GetAppUserSummaryByIdAsync(int userid, CancellationToken ct);

    /// <summary>
    /// دریافت اطلاعات کامل کاربر بر اساس شناسه.
    /// </summary>
    Task<Result<AppUserFullDto>> GetAppUserFullByIdAsync(int userId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست دسته‌بندی و کارهای متخصص.
    /// </summary>
    Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct);

    /// <summary>
    /// بروزرسانی اطلاعات کاربر.
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    /// <param name="dto">اطلاعات بروزرسانی</param>
    /// <param name="currentProfileImage">مسیر تصویر فعلی برای حذف در صورت موفقیت</param>
    /// <param name="ct">توکن لغو</param>
    Task<Result<bool>> UpdateAsync(int userId, AppUserUpdateDto dto, string? currentProfileImage, CancellationToken ct);

    /// <summary>
    /// حذف نرم کاربر بر اساس شناسه.
    /// </summary>
    Task<Result<bool>> SoftDeleteAsync(int userId, CancellationToken ct);

    /// <summary>
    /// بررسی ثبت شهر محل سکونت برای کاربر
    /// </summary>
    Task<bool> IsCitySetAsync(int userId, CancellationToken ct);
}
