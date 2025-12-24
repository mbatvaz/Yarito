using Yarito.Domain.Core.Entities.Categories;
using Yarito.Domain.Core.Entities.Images;
using Yarito.Domain.Core.Entities.Requests;

namespace Yarito.Domain.Core.Entities.Users;

/// <summary>
/// نماینده یک متخصص در سیستم است که به درخواست‌های خدماتی پاسخ می‌دهد.
/// </summary>
/// <remarks>
/// <para>این کلاس این کلاس ارتباط یک به یک با `AppUser` دارد و ویژگی‌های مختص متخصصان را اضافه می‌کند</para>
/// <para>ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>ExpertCategories:</b> لیست خدماتی که متخصص در آن‌ها فعالیت دارد</description></item>
/// <item><description><b>Bids:</b> لیست پیشنهادهای ثبت‌شده توسط متخصص</description></item>
/// <item><description><b>Reviews:</b> لیست نظراتی که برای این متخصص ثبت شده است</description></item>
/// </list>
/// </remarks>
public class Expert : AppUser
{
    // Navigation Properties
    public ICollection<ExpertCategory> ExpertCategories { get; set; } = [];
    public ICollection<ExpertImage> ExpertImages { get; set; } = [];
    public ICollection<Bid> Bids { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
}
