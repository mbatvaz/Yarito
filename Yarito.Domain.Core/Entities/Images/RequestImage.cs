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
/// <item><description><b>Url:</b> آدرس کامل تصویر</description></item>
/// </list>
/// </remarks>
public class RequestImage : BaseEntity
{
    // Properties
    public string Url { get; set; } = string.Empty;

    // Foreign Keys
    public Guid RequestId { get; set; }

    // Navigation Properties
    public Request Request { get; set; } = null!;
}