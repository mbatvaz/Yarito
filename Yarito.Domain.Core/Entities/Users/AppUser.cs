using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Cities;

namespace Yarito.Domain.Core.Entities.Users;

/// <summary>
/// نماینده یک کاربر در سیستم است که اطلاعات تجاری کاربر را نگهداری می‌کند.
/// این کلاس پایه برای انواع مختلف کاربران مانند مشتری و متخصص استفاده می‌شود.
/// </summary>
/// <remarks>
/// <para>این کلاس ویژگی‌های مشترک تمام کاربران سیستم را تعریف می‌کند.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Id:</b> شناسه کاربر که با Id کاربر Identity مطابقت دارد</description></item>
/// <item><description><b>FirstName:</b> نام کاربر</description></item>
/// <item><description><b>LastName:</b> نام خانوادگی کاربر</description></item>
/// <item><description><b>WalletBalance:</b> موجودی کیف پول کاربر</description></item>
/// <item><description><b>RegisteredAt:</b> تاریخ و زمان ثبت‌نام کاربر</description></item>
/// <item><description><b>ProfileImageUrl:</b> آدرس تصویر پروفایل کاربر</description></item>
/// <item><description><b>IsDeleted:</b> وضعیت حذف نرم کاربر</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>CityId:</b> شناسه شهر محل سکونت کاربر</description></item>
/// <item><description><b>City:</b> ارجاع به موجودیت شهر</description></item>
/// <item><description><b>Transactions:</b> لیست تراکنش‌های انجام‌شده توسط کاربر</description></item>
/// </list>
/// </remarks>
public abstract class AppUser : BaseEntity
{
    // Properties
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal WalletBalance { get; set; } = 0;
    public string? ProfileImageUrl { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }

    // Foreign Keys
    public Guid? CityId { get; set; }

    // Navigation Properties
    public City? City { get; set; }
}