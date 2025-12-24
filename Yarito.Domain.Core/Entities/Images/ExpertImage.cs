using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Entities.Images;
/// <summary>
/// نماینده یک تصویر در سیستم است که به متخصص مرتبط است.
/// </summary>
/// <remarks>
/// <para>این کلاس برای ذخیره اطلاعات تصاویر آپلودشده برای متخصص به عنوان نمونه کار در سیستم استفاده می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Url:</b> آدرس کامل تصویر</description></item>
/// </list>
/// </remarks>
public class ExpertImage : BaseEntity
{
    // Properties
    public required string Url { get; set; }

    // Foreign Keys
    public required Guid ExpertId { get; set; }

    // Navigation Properties
    public required Expert Expert { get; set; }
}
