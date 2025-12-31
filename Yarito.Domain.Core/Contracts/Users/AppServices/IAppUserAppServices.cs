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
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>آمار کلی سیستم</returns>
    Task<AppStatisticsDto> GetStatisticsAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه کاربران با قابلیت صفحه‌بندی.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از کاربران</returns>
    Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct);

    Task<Result<AppUserFullDto>> GetAppUserFullByIdAsync(int userId, CancellationToken ct);

    Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct);

    /// <summary>
    /// حذف نرم کاربر بر اساس شناسه.
    /// </summary>
    /// <param name="userId">شناسه کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات حذف</returns>
    Task<Result<string>> SoftDeleteAsync(int userId, CancellationToken ct);
}
