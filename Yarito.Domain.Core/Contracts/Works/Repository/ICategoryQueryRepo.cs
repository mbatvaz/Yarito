using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository;

/// <summary>
/// اینترفیس Query ریپازیتوری برای عملیات خواندن دسته‌بندی‌ها با Dapper.
/// </summary>
public interface ICategoryQueryRepo
{
    /// <summary>
    /// دریافت لیست ساده دسته‌بندی‌ها (فقط شناسه و عنوان).
    /// </summary>
    Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست تمام دسته‌بندی‌ها همراه با عناوین خدمات مربوطه.
    /// </summary>
    Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct);
}
