using System.ComponentModel.DataAnnotations;

namespace Yarito.Domain.Core.Enums.Requests;

/// <summary>
/// وضعیت‌های مختلف یک درخواست خدماتی را در طول چرخه حیات آن مشخص می‌کند。
/// </summary>
public enum RequestStatusEnum
{
    /// <summary>
    /// درخواست ثبت شده و در انتظار بررسی یا پذیرش توسط متخصصان است。
    /// </summary>
    [Display(Name = "در انتظار")]
    Pending = 0,

    /// <summary>
    /// درخواست توسط یک متخصص پذیرفته شده و در حال انجام است。
    /// </summary>
    [Display(Name = "جاری")]
    InProgress = 1,

    /// <summary>
    /// کار مربوط به درخواست با موفقیت به پایان رسیده است。
    /// </summary>
    [Display(Name = "تکمیل شده")]
    Completed = 2,

    /// <summary>
    /// درخواست توسط مشتری یا سیستم لغو شده است。
    /// </summary>
    [Display(Name = "لغو شده")]
    Cancelled = 3,
}