using Yarito.Domain.Core.DTOs.Works;
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
    Task<bool> AddAsync(Category newCategory, CancellationToken ct);

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
}
