using System.ComponentModel.DataAnnotations;

namespace Yarito.Domain.Core.Enums.Requests;

/// <summary>
/// وضعیت‌های مختلف یک پیشنهاد را که توسط متخصص برای یک درخواست ثبت می‌شود، مشخص می‌کند.
/// </summary>
public enum BidStatusEnum
{
    /// <summary>
    /// پیشنهاد ثبت شده و در انتظار تصمیم مشتری است.
    /// </summary>
    [Display(Name = "در انتظار")]
    Pending = 0,

    /// <summary>
    /// پیشنهاد توسط مشتری پذیرفته شده است.
    /// </summary>
    [Display(Name = "پذیرفته شده")]
    Accepted = 1,

    /// <summary>
    /// پیشنهاد توسط مشتری رد شده یا پیشنهاد دیگری برای درخواست پذیرفته شده است.
    /// </summary>
    [Display(Name = "رد شده")]
    Rejected = 2,

    /// <summary>
    /// متخصص پیشنهاد پذیرفته شده را انجام داده است.
    /// </summary>
    [Display(Name = "انجام شده")]
    Done = 3
}
