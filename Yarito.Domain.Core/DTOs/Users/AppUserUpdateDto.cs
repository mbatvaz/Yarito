namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// این مدل برای آپدیت اطلاعات کاربران استفاده می‌شود.
/// </summary>
public class AppUserUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public string? ProfileImgPath { get; set; }
    public Stream? ProfileImage { get; set; }
    public string? ProfileImageExtension { get; set; }
    public bool DeleteProfileImage { get; set; }
}