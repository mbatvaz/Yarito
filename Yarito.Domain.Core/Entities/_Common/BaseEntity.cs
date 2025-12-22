namespace Yarito.Domain.Core.Entities._Common;

/// <summary>
/// کلاس پایه برای تمام موجودیت‌های سیستم که ویژگی‌های مشترک را فراهم می‌کند.
/// </summary>
/// <remarks>
/// <para>این کلاس شامل شناسه‌ی یکتا، تاریخ ایجاد و وضعیت حذف نرم است.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Id:</b> شناسه یکتای موجودیت (GUID)</description></item>
/// <item><description><b>IsDeleted:</b> فلگی برای پیاده‌سازی حذف نرم (Soft Delete)</description></item>
/// <item><description><b>CreatedAt:</b> تاریخ و زمان ایجاد موجودیت به وقت جهانی (UTC)</description></item>
/// </list>
/// </remarks>
public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
