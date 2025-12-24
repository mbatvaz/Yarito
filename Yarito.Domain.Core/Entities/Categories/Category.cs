using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;

namespace Yarito.Domain.Core.Entities.Categories;
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
/// <item><description><b>BasePrice:</b> قیمت پایه خدمت در این دسته‌بندی</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>ParentId:</b> شناسه دسته‌بندی والد</description></item>
/// <item><description><b>Parent:</b> ارجاع به موجودیت والد</description></item>
/// <item><description><b>Children:</b> لیست دسته‌بندی‌های فرزند</description></item>
/// </list>
/// </remarks>
public class Category : BaseEntity
{
    // Properties
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }

    // Foreign Keys
    public Guid? ParentId { get; set; }

    // Navigation Properties
    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = [];
    public ICollection<Request> Requests { get; set; } = [];
    public ICollection<ExpertCategory> ExpertCategories { get; set; } = [];
}
