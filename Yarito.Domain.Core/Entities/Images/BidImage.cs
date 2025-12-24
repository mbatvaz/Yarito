using Yarito.Domain.Core.Entities.Requests;

namespace Yarito.Domain.Core.Entities.Images;
/// <summary>
/// نماینده یک تصویر متخصص که برای یک پیشنهاد انتخاب شده.
/// </summary>
/// <remarks>
/// <para>این کلاس یک کلاس واسط بین تصاویر نمونه کار متخصص و پیشنهادی که برای یک درخواست ارسال میکند است</para>
/// </remarks>
public class BidImage
{
    // Foreign Keys
    public Guid BidId { get; set; }
    public Guid ExpertImageId { get; set; }

    // Navigation Properties
    public Bid Bid { get; set; } = null!;
    public ExpertImage ExpertImage { get; set; } = null!;
}
