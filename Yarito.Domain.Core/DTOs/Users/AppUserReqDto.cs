using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Domain.Core.DTOs.Users;

/// <summary>
/// مدل درخواست برای جستجو و فیلتر کاربران.
/// </summary>
public class AppUserReqDto : PageRequest
{
    public UserTypeEnum? UserType { get; set; }

    public int? CityId { get; init; }
    public string? TextSearch { get; set; }

    public SortRequest<AppUserSortableEnum>? Sort { get; init; }
}
