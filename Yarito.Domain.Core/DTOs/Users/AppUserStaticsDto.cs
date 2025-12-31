namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// مدل انتقال داده برای آمار تعداد کاربران.
/// </summary>
public class AppUserStaticsDto
{
    public int NumberOfCustomers { get; set; }
    public int NumberOfExperts { get; set; }
}
