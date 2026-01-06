using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Works.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با دسته‌بندی‌ها.
/// </summary>
public interface ICategoryServices
{
    #region Query Methods

    /// <summary>
    /// دریافت یک دسته‌بندی بر اساس شناسه.
    /// </summary>
    Task<CategoryDto?> GetByIdAsync(int categoryId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست تمام دسته‌بندی‌ها با عناوین خدمات مربوطه.
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
    /// افزودن یک دسته‌بندی جدید.
    /// </summary>
    Task<Result<CategoryDto>> AddAsync(CategoryDto newCategory, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی یک دسته‌بندی موجود.
    /// </summary>
    Task<Result<CategoryDto>> UpdateAsync(CategoryDto category, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک دسته‌بندی.
    /// </summary>
    Task<Result<bool>> DeleteAsync(int categoryId, CancellationToken ct);

    #endregion

    #region Validation Methods

    /// <summary>
    /// اعتبارسنجی ویژگی‌های دسته‌بندی.
    /// </summary>
    Result<CategoryDto> IsPropertyValid(CategoryDto dto);

    /// <summary>
    /// بررسی تکراری بودن عنوان دسته‌بندی.
    /// </summary>
    Task<Result<CategoryDto>> IsTitleDuplicationAsync(string title, CancellationToken ct, int? categoryId = null);

    #endregion
}
