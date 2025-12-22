using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Entities.Cities;

/// <summary>
/// نماینده یک شهر یا استان است. اگر یک شهر والد نداشته باشد، به عنوان استان در نظر گرفته می‌شود。
/// </summary>
/// <remarks>
/// <para>این کلاس برای مدیریت ساختار سلسله‌مراتبی شهرها و استان‌ها استفاده می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Name:</b> نام شهر یا استان</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>ParentId:</b> شناسه والد (استان)</description></item>
/// <item><description><b>Parent:</b> ارجاع به موجودیت والد</description></item>
/// <item><description><b>Children:</b> لیست شهرهای زیرمجموعه این استان</description></item>
/// <item><description><b>Users:</b> لیست کاربرانی که در این شهر سکونت دارند</description></item>
/// </list>
/// </remarks>
public class City : BaseEntity
{
    // Properties
    public required string Name { get; set; }

    // Foreign Keys
    public Guid? ParentId { get; set; }

    // Navigation Properties
    public City? Parent { get; set; }
    public ICollection<City> Children { get; set; } = [];
    public ICollection<AppUser> Users { get; set; } = [];
}
