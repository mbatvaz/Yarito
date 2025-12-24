using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Images;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Entities.Requests;

/// <summary>
/// نماینده یک پیشنهاد است که توسط یک متخصص برای یک درخواست خدماتی ثبت می‌شود.
/// </summary>
/// <remarks>
/// <para>این کلاس نمایانگر پیشنهادات مالی است که توسط متخصصان برای انجام یک درخواست خاص ارائه می‌شود.</para>
/// <para>هر متخصص فقط میتواند به هر درخواست فقط یک پیشنهاد ثبت کند.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Description:</b> توضیحات متخصص در مورد پیشنهاد</description></item>
/// <item><description><b>ProposedPrice:</b> قیمت پیشنهادی متخصص</description></item>
/// <item><description><b>ProposedVisitDateTime:</b> تاریخ و زمان پیشنهادی متخصص برای حضور</description></item>
/// <item><description><b>Status:</b> وضعیت فعلی پیشنهاد (مانند در حال بررسی، تایید شده)</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>RequestId:</b> شناسه درخواستی که این پیشنهاد برای آن ثبت شده</description></item>
/// <item><description><b>Request:</b> ارجاع به موجودیت درخواست</description></item>
/// <item><description><b>ExpertId:</b> شناسه متخصص ثبت‌کننده پیشنهاد</description></item>
/// <item><description><b>Expert:</b> ارجاع به موجودیت متخصص</description></item>
/// <item><description><b>ReviewId:</b> شناسه نظر ثبت‌شده برای این پیشنهاد</description></item>
/// <item><description><b>Review:</b> ارجاع به نظر ثبت‌شده</description></item>
/// <item><description><b>ExpertImages:</b> لیست تصاویر مرتبط متخصص با این پیشنهاد</description></item>
/// </list>
/// </remarks>
public class Bid : BaseEntity
{
    // Properties
    public string? Description { get; set; }
    public decimal ProposedPrice { get; set; }
    public DateTime? ProposedVisitDateTime { get; set; }
    public BidStatusEnum Status { get; set; } = BidStatusEnum.Pending;

    // Foreign Keys
    public Guid RequestId { get; set; }
    public Guid ExpertId { get; set; }
    public Guid? ReviewId { get; set; }

    // Navigation Properties
    public Request Request { get; set; } = null!;
    public ICollection<BidImage> BidImage { get; set; } = [];
    public Expert Expert { get; set; } = null!;
    public Review? Review { get; set; }
}
