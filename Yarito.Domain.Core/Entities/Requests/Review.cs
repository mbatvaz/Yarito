using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Entities.Requests;

/// <summary>
/// نماینده یک نظر است که توسط مشتری پس از اتمام کار برای یک پیشنهاد ثبت می‌شود.
/// </summary>
/// <remarks>
/// <para>این کلاس برای ثبت نظرات و امتیازات مشتریان در مورد خدمات ارائه‌شده توسط متخصصان استفاده می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Rating:</b> امتیاز ثبت‌شده توسط مشتری (از 1 تا 5)</description></item>
/// <item><description><b>Comment:</b> متن نظر مشتری</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>CustomerId:</b> شناسه مشتری ثبت‌کننده نظر</description></item>
/// <item><description><b>Customer:</b> ارجاع به موجودیت مشتری</description></item>
/// <item><description><b>RequestId:</b> شناسه درخواستی که این نظر برای آن ثبت شده</description></item>
/// <item><description><b>Request:</b> ارجاع به موجودیت درخواست</description></item>
/// <item><description><b>ExpertId:</b> شناسه متخصصی که این نظر برای او ثبت شده</description></item>
/// <item><description><b>Expert:</b> ارجاع به موجودیت متخصص</description></item>
/// </list>
/// </remarks>
public class Review : BaseEntity
{
    // Properties
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public ReviewStatusEnum ReviewStatus { get; set; } = ReviewStatusEnum.Pending;

    // Foreign Keys
    public int RequestId { get; set; }
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }

    // Navigation Properties
    public Request Request { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public Expert Expert { get; set; } = null!;
}