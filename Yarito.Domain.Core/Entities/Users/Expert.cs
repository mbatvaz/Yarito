using Yarito.Domain.Core.Entities.Categories;
using Yarito.Domain.Core.Entities.Requests;

namespace Yarito.Domain.Core.Entities.Users;

/// <summary>
/// نماینده یک متخصص در سیستم است که به درخواست‌های خدماتی پاسخ می‌دهد.
/// </summary>
/// <remarks>
/// <para>این کلاس از `AppUser` ارث‌بری می‌کند و ویژگی‌های مختص متخصصان را اضافه می‌کند.</para>
/// <para>ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>Categories:</b> لیست دسته‌بندی‌هایی که متخصص در آن‌ها فعالیت دارد</description></item>
/// <item><description><b>Bids:</b> لیست پیشنهادهای ثبت‌شده توسط متخصص</description></item>
/// <item><description><b>Reviews:</b> لیست نظراتی که برای این متخصص ثبت شده است</description></item>
/// </list>
/// </remarks>
public class Expert : AppUser
{
    // Navigation Properties
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Bid> Bids { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
}
