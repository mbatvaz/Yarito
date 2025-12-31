using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Contracts.Users.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی متخصصان.
/// </summary>
public interface IExpertRepo
{
    /// <summary>
    /// افزودن یک متخصص جدید به دیتابیس.
    /// </summary>
    /// <param name="newExpert">موجودیت متخصص جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(Expert newExpert, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک متخصص موجود.
    /// </summary>
    /// <param name="newExpert">موجودیت متخصص با اطلاعات جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(Expert newExpert, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک متخصص از دیتابیس.
    /// </summary>
    /// <param name="expertId">شناسه متخصص</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(int expertId, CancellationToken ct);
}
