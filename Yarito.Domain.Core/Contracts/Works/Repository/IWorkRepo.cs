using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی خدمات.
/// </summary>
public interface IWorkRepo
{
    #region Query Methods

    /// <summary>
    /// دریافت یک خدمت بر اساس شناسه.
    /// </summary>
    Task<WorkDto?> GetByIdAsync(int workId, CancellationToken ct);

    /// <summary>
    /// گرفتن لیستی از شناسه کارها و برگرداندن لیستی از اطلاعات کامل آن‌ها.
    /// </summary>
    Task<List<WorksFullDto>> GetWorksByIDs(List<int> ids, CancellationToken ct);

    #endregion

    #region Command Methods

    /// <summary>
    /// افزودن یک خدمت جدید به دیتابیس.
    /// </summary>
    Task<bool> AddAsync(WorkDto newWork, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک خدمت موجود.
    /// </summary>
    Task<bool> UpdateAsync(WorkDto work, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک خدمت از دیتابیس.
    /// </summary>
    Task<bool> DeleteAsync(int workId, CancellationToken ct);

    #endregion

    #region Validation Methods

    /// <summary>
    /// بررسی وجود عنوان تکراری (برای افزودن).
    /// </summary>
    Task<bool> IsTitleExistsAsync(string title, CancellationToken ct);

    /// <summary>
    /// بررسی وجود عنوان تکراری (برای ویرایش - با استثنا کردن خدمت فعلی).
    /// </summary>
    Task<bool> IsTitleExistsAsync(string title, int excludeId, CancellationToken ct);

    /// <summary>
    /// بررسی استفاده از دسته‌بندی در خدمات.
    /// </summary>
    Task<bool> IsCategoryInUseAsync(int categoryId, CancellationToken ct);

    #endregion
}
