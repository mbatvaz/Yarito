namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// مدل انتقال داده برای نمایش خلاصه اطلاعات کاربر در داشبورد.
/// </summary>
public class UserDashboardDto
{
    public required decimal WalletBalance { get; init; }
    public required bool IsInfoComplete { get; init; }
}
