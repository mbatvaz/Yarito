using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Categories;
using Yarito.Domain.Core.Entities.Images;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Entities.Requests;

/// <summary>
/// نماینده یک درخواست خدماتی است که توسط مشتری ایجاد می‌شود.
/// </summary>
/// <remarks>
/// <para>این کلاس شامل اطلاعات کامل یک درخواست است که توسط مشتری ثبت می‌شود.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Title:</b> عنوان درخواست</description></item>
/// <item><description><b>Description:</b> توضیحات تکمیلی درخواست</description></item>
/// <item><description><b>ProposedPrice:</b> قیمت پیشنهادی مشتری برای انجام کار</description></item>
/// <item><description><b>Address:</b> آدرس محل انجام درخواست</description></item>
/// <item><description><b>PreferredVisitDateTime:</b> تاریخ و زمان ترجیحی مشتری برای حضور متخصص</description></item>
/// <item><description><b>Status:</b> وضعیت فعلی درخواست (مانند در حال بررسی، تایید شده و...)</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>CustomerId:</b> شناسه مشتری ثبت‌کننده درخواست</description></item>
/// <item><description><b>Customer:</b> ارجاع به موجودیت مشتری</description></item>
/// <item><description><b>CategoryId:</b> شناسه دسته‌بندی خدمت</description></item>
/// <item><description><b>Category:</b> ارجاع به موجودیت دسته‌بندی</description></item>
/// <item><description><b>AcceptedBidId:</b> شناسه پیشنهاد پذیرفته‌شده (در صورتی که وجود داشته باشد)</description></item>
/// <item><description><b>AcceptedBid:</b> ارجاع به پیشنهاد پذیرفته‌شده</description></item>
/// <item><description><b>Review:</b> ارجاع به نظر ثبت‌شده برای این درخواست</description></item>
/// <item><description><b>Bids:</b> لیست تمام پیشنهادهای ثبت‌شده برای این درخواست</description></item>
/// <item><description><b>RequestImages:</b> لیست تصاویر مرتبط با این درخواست</description></item>
/// </list>
/// </remarks>
public class Request : BaseEntity
{
    // Properties
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal? ProposedPrice { get; set; }
    public required string Address { get; set; }
    public DateTime? PreferredVisitDateTime { get; set; }
    public RequestStatusEnum Status { get; set; } = RequestStatusEnum.Pending;

    // Foreign Keys
    public Guid CustomerId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? AcceptedBidId { get; set; }

    // Navigation Properties
    public Customer Customer { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public Bid? AcceptedBid { get; set; }
    public ICollection<Bid> Bids { get; set; } = [];
    public ICollection<RequestImage> RequestImages { get; set; } = [];
    public Review? Review { get; set; }
}
