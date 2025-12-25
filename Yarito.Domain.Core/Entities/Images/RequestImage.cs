using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;


namespace Yarito.Domain.Core.Entities.Images;
/// <summary>
/// نماینده یک تصویر در سیستم است که به درخواست مرتبط است.
/// </summary>
/// <remarks>
/// <para>این کلاس برای ذخیره اطلاعات تصاویر آپلودشده برای درخواست در سیستم استفاده می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>ImgPath:</b> مسیر تصویر</description></item>
/// </list>
/// </remarks>
public class RequestImage : BaseEntity
{
    // Properties
    public required string ImgPath { get; set; }

    // Foreign Keys
    public int RequestId { get; set; }

    // Navigation Properties
    public Request Request { get; set; } = null!;
}