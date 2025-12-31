using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Users.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات احراز هویت کاربران.
/// </summary>
public interface IAuthenticationAppServices
{
    /// <summary>
    /// ورود کاربر به سیستم.
    /// </summary>
    /// <param name="dto">اطلاعات ورود شامل شماره تلفن و رمز عبور</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات ورود با پیام مناسب</returns>
    Task<Result<string>> LoginAsync(LoginDto dto, CancellationToken ct);

    /// <summary>
    /// خروج کاربر از سیستم.
    /// </summary>
    /// <returns>نتیجه عملیات خروج</returns>
    Task<Result<string>> LogoutAsync();

    /// <summary>
    /// ثبت‌نام کاربر جدید در سیستم.
    /// </summary>
    /// <param name="dto">اطلاعات ثبت‌نام کاربر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه عملیات ثبت‌نام با پیام مناسب</returns>
    Task<Result<string>> RegisterAsync(RegisterDto dto, CancellationToken ct);
}
