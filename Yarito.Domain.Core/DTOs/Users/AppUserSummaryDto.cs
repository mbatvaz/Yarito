using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// مدل انتقال داده برای نمایش خلاصه اطلاعات کاربر.
/// </summary>
public class AppUserSummaryDto
{
    public required int Id { get; init; }
    public required string? ProfileImgPath { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
    public required UserTypeEnum UserType { get; init; }
    public required decimal WalletBalance { get; init; }
    public required bool IsInfoComplete { get; init; } 
}
