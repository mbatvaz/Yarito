using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Entities.Works;
/// <summary>
/// نماینده یک خدمت است که توسط متخصصان ارائه می‌شود.
/// </summary>
/// <remarks>
/// <para>این کلاس خدماتی که متخصصان می‌توانند ارائه دهند را نمایش می‌دهد.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Title:</b> عنوان خدمت</description></item>
/// <item><description><b>BasePrice:</b> قیمت پایه خدمت</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>CategoryId:</b> شناسه دسته‌بندی که این خدمت در آن قرار دارد</description></item>
/// <item><description><b>Category:</b> ارجاع به موجودیت دسته‌بندی</description></item>
/// <item><description><b>Experts:</b> لیست متخصصانی که این خدمت را ارائه می‌دهند</description></item>
/// <item><description><b>Requests:</b> لیست درخواست‌های مرتبط با این خدمت</description></item>
/// </list>
/// </remarks>
public class Work : BaseEntity
{
    // Properties
    public required string Title { get; set; }
    public decimal BasePrice { get; set; }

    // Foreign Keys
    public int CategoryId { get; set; }

    // Navigation Properties
    public Category Category { get; set; } = null!;
    public ICollection<ExpertWork> ExpertWorks { get; set; } = [];
    public ICollection<Request> Requests { get; set; } = [];
}
