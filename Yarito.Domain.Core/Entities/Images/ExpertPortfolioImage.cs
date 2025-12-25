using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Entities.Images;
/// <summary>
/// نماینده یک تصویر از نمونه کارهای متخصص است.
/// </summary>
/// <remarks>
/// <para>این کلاس برای ذخیره اطلاعات تصاویر نمونه کار متخصصان در سیستم استفاده می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>ImgPath:</b> مسیر تصویر نمونه کار</description></item>
/// <item><description><b>Title:</b> عنوان یا توضیح کوتاه تصویر</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>ExpertId:</b> شناسه متخصصی که این تصویر متعلق به اوست</description></item>
/// <item><description><b>Expert:</b> ارجاع به موجودیت متخصص</description></item>
/// </list>
/// </remarks>
public class ExpertPortfolioImage : BaseEntity
{
    // Properties
    public required string ImgPath { get; set; }
    public string? Title { get; set; } 

    // Foreign Keys
    public int ExpertId { get; set; }

    // Navigation Properties
    public Expert Expert { get; set; } = null!;
}
