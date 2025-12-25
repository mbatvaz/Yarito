using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Entities.Works;
/// <summary>
/// نماینده یک دسته‌بندی برای خدمات است. هر دسته‌بندی می‌تواند یک والد داشته باشد و به این ترتیب یک ساختار سلسله‌مراتبی ایجاد می‌شود.
/// خدماتی که والد ندارند، به عنوان دسته‌بندی اصلی در نظر گرفته می‌شوند.
/// </summary>
/// <remarks>
/// <para>این کلاس برای مدیریت دسته‌بندی‌های خدمات به صورت سلسله‌مراتبی استفاده می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Title:</b> عنوان دسته‌بندی</description></item>
/// <item><description><b>Description:</b> توضیحات اختیاری دسته‌بندی</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>Works:</b> لیست خدماتی که در این دسته‌بندی قرار دارند</description></item>
/// </list>
/// </remarks>
public class Category : BaseEntity
{
    // Properties
    public required string Title { get; set; }
    public string? Description { get; set; }

    // Navigation Properties
    public ICollection<Work> Works { get; set; } = [];
}
