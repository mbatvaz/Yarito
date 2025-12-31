using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Core.Contracts.Works.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با دسته‌بندی‌ها.
/// </summary>
public interface ICategoryServices
{
    /// <summary>
    /// دریافت لیست تمام دسته‌بندی‌ها با عناوین خدمات مربوطه.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست دسته‌بندی‌ها</returns>
    Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct);
}
