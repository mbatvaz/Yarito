using Yarito.Domain.Core.Entities.Requests;

namespace Yarito.Domain.Core.Entities.Users;

/// <summary>
/// نماینده یک مشتری در سیستم است که می‌تواند درخواست‌های خدماتی ثبت کند.
/// </summary>
/// <remarks>
/// <para>این کلاس از `AppUser` ارث‌بری می‌کند و ویژگی‌های مختص مشتریان را اضافه می‌کند.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Address:</b> آدرس محل سکونت مشتری</description></item>
/// </list>
/// <para>ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>Requests:</b> لیست درخواست‌های ثبت‌شده توسط مشتری</description></item>
/// <item><description><b>Reviews:</b> لیست نظرات ثبت‌شده توسط مشتری</description></item>
/// </list>
/// </remarks>
public class Customer : AppUser
{
    // Properties
    public string? Address { get; set; }

    // Navigation Properties
    public ICollection<Request> Requests { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
}
