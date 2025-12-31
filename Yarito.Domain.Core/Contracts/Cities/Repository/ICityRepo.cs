using Yarito.Domain.Core.Entities.Cities;

namespace Yarito.Domain.Core.Contracts.Cities.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی شهرها و استان‌ها.
/// </summary>
public interface ICityRepo
{
    /// <summary>
    /// افزودن یک شهر یا استان جدید به دیتابیس.
    /// </summary>
    /// <param name="newCity">موجودیت شهر جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(City newCity, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک شهر یا استان موجود.
    /// </summary>
    /// <param name="newCity">موجودیت شهر با اطلاعات جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(City newCity, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک شهر یا استان از دیتابیس.
    /// </summary>
    /// <param name="cityId">شناسه شهر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(int cityId, CancellationToken ct);
}
