using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Users.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با کاربران.
/// </summary>
public interface IAppUserServices
{
    #region Query Methods

    /// <summary>
    /// دریافت آمار تعداد کاربران سیستم.
    /// </summary>
    Task<AppUserStaticsDto> GetUserCountAsync(CancellationToken ct);

    /// <summary>
    /// دریافت نام کاربر بر اساس شناسه.
    /// </summary>
    Task<Result<AppUserSummaryDto>> GetAppUserSummaryByIdAsync(int userId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست خلاصه کاربران با قابلیت صفحه‌بندی.
    /// </summary>
    Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت اطلاعات کامل کاربر بر اساس شناسه کاربر.
    /// </summary>
    Task<Result<AppUserFullDto>> GetAppUserFullByIdAsync(int userId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست دسته‌بندی و کارهای متخصص.
    /// </summary>
    Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct);

    #endregion

    #region Command Methods

    /// <summary>
    /// ثبت کاربر جدید در سیستم.
    /// </summary>
    Task<Result<RegisterDto>> AddAsync(RegisterDto dto, CancellationToken ct);

    /// <summary>
    /// بروزرسانی اطلاعات کاربر.
    /// </summary>
    Task<Result<bool>> UpdateAsync(int userId, AppUserUpdateDto dto, CancellationToken ct);

    /// <summary>
    /// حذف نرم کاربر بر اساس شناسه.
    /// </summary>
    Task<Result<bool>> SoftDeleteAsync(int userId, CancellationToken ct);

    #endregion

    #region Validation Methods

    /// <summary>
    /// اعتبارسنجی ویژگی‌های کاربر.
    /// </summary>
    Result<RegisterDto> IsPropertyValid(RegisterDto dto);

    /// <summary>
    /// اعتبارسنجی ویژگی‌های کاربر برای بروزرسانی.
    /// </summary>
    Result<AppUserUpdateDto> IsPropertyValid(AppUserUpdateDto dto);

    /// <summary>
    /// بررسی تکراری بودن ایمیل.
    /// </summary>
    Task<Result<RegisterDto>> IsEmailDuplicationAsync(string email, CancellationToken ct, int? userId = null);

    /// <summary>
    /// بررسی وجود کاربر.
    /// </summary>
    Task<bool> IsExistsAsync(int userId, CancellationToken ct);

    /// <summary>
    /// بررسی ثبت شهر محل سکونت برای کاربر
    /// </summary>
    Task<bool> IsCitySetAsync(int userId, CancellationToken ct);


    /// <summary>
    /// نتیجه جستجوی آدرس مشتری را بر میگرداند
    /// </summary>
    Task<Result<string>> GetCustomerAddressAsync(int userId, CancellationToken ct);

    #endregion

    Task<Result<bool>> IncreaseWalletBalanceAsync(int userId, decimal amount, CancellationToken ct, bool save = true);

    Task<Result<bool>> DecreaseWalletBalanceAsync(int userId, decimal amount, CancellationToken ct, bool save = true);
}
