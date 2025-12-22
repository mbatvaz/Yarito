using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Images;

namespace Yarito.Domain.Core.Entities.Images;

/// <summary>
/// نماینده یک تصویر در سیستم است که می‌تواند به موجودیت‌های مختلفی مانند درخواست یا پیشنهاد مرتبط باشد.
/// </summary>
/// <remarks>
/// <para>این کلاس برای ذخیره اطلاعات تصاویر آپلودشده در سیستم استفاده می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Url:</b> آدرس کامل تصویر</description></item>
/// <item><description><b>ImageType:</b> نوع تصویر که مشخص می‌کند به کدام بخش از سیستم تعلق دارد (مانند تصویر پروفایل، تصویر درخواست)</description></item>
/// <item><description><b>RelatedEntityId:</b> شناسه موجودیتی که این تصویر به آن مرتبط است (مانند شناسه درخواست یا پیشنهاد)</description></item>
/// </list>
/// </remarks>
public class Image : BaseEntity
{
    // Properties
    public required string Url { get; set; }
    public ImageTypeEnum ImageType { get; set; }
    public Guid RelatedEntityId { get; set; }
}
