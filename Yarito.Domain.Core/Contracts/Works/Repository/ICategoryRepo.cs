using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Works.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی دسته‌بندی‌های خدمات.
/// </summary>
public interface ICategoryRepo
{
    #region Query Methods

    /// <summary>
    /// دریافت یک دسته‌بندی بر اساس شناسه.
    /// </summary>
    Task<CategoryDto?> GetByIdAsync(int categoryId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست تمام دسته‌بندی‌ها همراه با عناوین خدمات مربوطه.
    /// </summary>
    Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست ساده دسته‌بندی‌ها (فقط شناسه و عنوان).
    /// </summary>
    Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct);

    /// <summary>
    /// دریافت لیست دسته‌بندی‌ها با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct);

    #endregion

    #region Command Methods

    /// <summary>
    /// افزودن یک دسته‌بندی جدید به دیتابیس.
    /// </summary>
    Task<bool> AddAsync(CategoryDto newCategory, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی یک دسته‌بندی موجود.
    /// </summary>
    Task<bool> UpdateAsync(CategoryDto category, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک دسته‌بندی از دیتابیس.
    /// </summary>
    Task<bool> DeleteAsync(int categoryId, CancellationToken ct);

    #endregion

    #region Validation Methods

    /// <summary>
    /// بررسی وجود عنوان تکراری (برای افزودن).
    /// </summary>
    Task<bool> IsTitleExistsAsync(string title, CancellationToken ct);

    /// <summary>
    /// بررسی وجود عنوان تکراری (برای ویرایش - با استثنا کردن دسته‌بندی فعلی).
    /// </summary>
    Task<bool> IsTitleExistsAsync(string title, int excludeId, CancellationToken ct);

    #endregion
}
