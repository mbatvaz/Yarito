using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی خدمات.
/// </summary>
public interface IWorkRepo
{
    /// <summary>
    /// افزودن یک خدمت جدید به دیتابیس.
    /// </summary>
    /// <param name="newWork">موجودیت خدمت جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(Work newWork, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک خدمت موجود.
    /// </summary>
    /// <param name="newWork">موجودیت خدمت با اطلاعات جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(Work newWork, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک خدمت از دیتابیس.
    /// </summary>
    /// <param name="workId">شناسه خدمت</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(int workId, CancellationToken ct);
}
