using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// این مدل برای آپدیت اطلاعات کاربران استفاده می‌شود.
/// </summary>
public class AppUserUpdateDto
{
    public required int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public string? ProfileImgPath { get; set; }
    public Stream? ProfileImage { get; set; }
    public string? ProfileImageFormat { get; set; }
    public string? CurrentProfileImage { get; set; }
    public bool DeleteProfileImage { get; set; }
    public UserTypeEnum? UserType { get; set; }
    public List<int>? WorkIds { get; set; }
    public List<WorksFullDto>? WorksFull { get; set; }
}
