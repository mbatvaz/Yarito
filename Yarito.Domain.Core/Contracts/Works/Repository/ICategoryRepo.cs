using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی دسته‌بندی‌های خدمات.
/// </summary>
public interface ICategoryRepo
{
    /// <summary>
    /// افزودن یک دسته‌بندی جدید به دیتابیس.
    /// </summary>
    /// <param name="newCategory">موجودیت دسته‌بندی جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(CategoryDto newCategory, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک دسته‌بندی از دیتابیس.
    /// </summary>
    /// <param name="categoryId">شناسه دسته‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(int categoryId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست تمام دسته‌بندی‌ها همراه با عناوین خدمات مربوطه.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست دسته‌بندی‌ها با اطلاعات متنی</returns>
    Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست دسته‌بندی‌ها با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از دسته‌بندی‌ها</returns>
    Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct);

    /// <summary>
    /// دریافت یک دسته‌بندی بر اساس شناسه.
    /// </summary>
    /// <param name="categoryId">شناسه دسته‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات دسته‌بندی یا null در صورت عدم وجود</returns>
    Task<CategoryDto?> GetByIdAsync(int categoryId, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی یک دسته‌بندی موجود.
    /// </summary>
    /// <param name="category">اطلاعات دسته‌بندی برای به‌روزرسانی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(CategoryDto category, CancellationToken ct);

    Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct);
}
