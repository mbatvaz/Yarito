using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// مدل انتقال داده برای ثبت‌نام کاربر جدید.
/// </summary>
public class RegisterDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public required string PhoneNumber { get; init; }
    public required string Password { get; init; }
    public required UserTypeEnum UserType { get; init; }
    public string? Email { get; init; }
    public decimal BaseWalletBalance { get; init; } = 0;
    public int? CityId { get; init; }
    public string? Address { get; set; }
    public Stream? ProfileImage { get; init; }
    public string? ProfileImageUrl { get; set; }
}
