namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// مدل انتقال داده برای ورود کاربر به سیستم.
/// </summary>
public class LoginDto
{
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
    public required bool RememberMe { get; set; }
}
