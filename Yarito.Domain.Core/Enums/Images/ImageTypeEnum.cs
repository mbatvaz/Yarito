using System.ComponentModel.DataAnnotations;

namespace Yarito.Domain.Core.Enums.Images;

/// <summary>
/// نوع تصویر را برای دسته‌بندی و مدیریت تصاویر در بخش‌های مختلف سیستم مشخص می‌کند.
/// </summary>
public enum ImageTypeEnum
{
    /// <summary>
    /// تصویر مرتبط با یک درخواست خدماتی
    /// </summary>
    [Display(Name = "درخواست")]
    Request = 0,

    /// <summary>
    /// تصویر مرتبط با یک پیشنهاد متخصص
    /// </summary>
    [Display(Name = "پیشنهاد")]
    Bid = 1,

    /// <summary>
    /// تصویر پروفایل کاربر
    /// </summary>
    [Display(Name = "پروفایل")]
    Profile = 2,
}
