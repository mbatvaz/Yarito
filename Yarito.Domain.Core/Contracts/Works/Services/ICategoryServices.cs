using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

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

    /// <summary>
    /// دریافت لیست دسته‌بندی‌ها با قابلیت صفحه‌بندی و فیلتر.
    /// </summary>
    /// <param name="q">پارامترهای جستجو و صفحه‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه صفحه‌بندی شده از دسته‌بندی‌ها</returns>
    Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct);

    /// <summary>
    /// افزودن یک دسته‌بندی جدید.
    /// </summary>
    /// <param name="newCategory">اطلاعات دسته‌بندی جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(CategoryDto newCategory, CancellationToken ct);

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

    /// <summary>
    /// حذف نرم یک دسته‌بندی.
    /// </summary>
    /// <param name="categoryId">شناسه دسته‌بندی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(int categoryId, CancellationToken ct);


    Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct);
}
