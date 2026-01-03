using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// مدل انتقال داده برای ثبت‌نام کاربر جدید.
/// </summary>
public class RegisterDto
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public UserTypeEnum UserType { get; init; }
    public string? Email { get; init; }
    public decimal BaseWalletBalance { get; init; } = 0;
    public int? CityId { get; init; }
    public string? Address { get; init; }
    public Stream? ProfileImage { get; init; }
    public string? ProfileImageUrl { get; set; }
}
