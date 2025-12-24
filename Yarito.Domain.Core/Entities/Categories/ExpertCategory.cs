using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Entities.Categories;
/// <summary>
/// نماینده مهارت هایی که متخصصان دارند است.
/// </summary>
/// <remarks>
/// <para>این کلاس برای مدیریت خدماتی است که متخصصین ارائه میدهد
/// زیرا هر متخصص میتواند چندین خدمت را ارائه دهد.</para>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>ExpertId:</b> شناسه متخصص</description></item>
/// <item><description><b>CategoryId:</b> شناسه دسته‌بندی</description></item>
/// </list>
/// </remarks>
public class ExpertCategory : BaseEntity
{
    // Foreign Keys
    public Guid ExpertId { get; set; }
    public Guid CategoryId { get; set; }

    // Navigation Properties
    public Expert Expert { get; set; } = null!;
    public Category Category { get; set; } = null!;
}