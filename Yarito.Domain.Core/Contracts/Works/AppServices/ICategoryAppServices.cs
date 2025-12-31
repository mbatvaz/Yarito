using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Core.Contracts.Works.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با دسته‌بندی‌ها در لایه نمایش.
/// </summary>
public interface ICategoryAppServices
{
    /// <summary>
    /// دریافت لیست تمام دسته‌بندی‌ها با عناوین خدمات مربوطه.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست دسته‌بندی‌ها</returns>
    Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct);
}
