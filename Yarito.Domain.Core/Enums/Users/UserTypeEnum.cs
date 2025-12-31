using System.ComponentModel.DataAnnotations;

namespace Yarito.Domain.Core.Enums.Users;

/// <summary>
/// نوع کاربر در سیستم را مشخص می‌کند.
/// </summary>
public enum UserTypeEnum
{
    /// <summary>
    /// کاربر از نوع مشتری که درخواست خدمات می‌دهد.
    /// </summary>
    [Display(Name = "مشتری")]
    Customer,

    /// <summary>
    /// کاربر از نوع متخصص که خدمات ارائه می‌دهد.
    /// </summary>
    [Display(Name = "متخصص")]
    Expert
}
