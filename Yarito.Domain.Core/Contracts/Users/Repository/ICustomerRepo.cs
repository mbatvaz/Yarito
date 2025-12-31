using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Contracts.Users.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات دیتابیسی مشتریان.
/// </summary>
public interface ICustomerRepo
{
    /// <summary>
    /// افزودن یک مشتری جدید به دیتابیس.
    /// </summary>
    /// <param name="newCustomer">موجودیت مشتری جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> AddAsync(Customer newCustomer, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی اطلاعات یک مشتری موجود.
    /// </summary>
    /// <param name="newCustomer">موجودیت مشتری با اطلاعات جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> UpdateAsync(Customer newCustomer, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک مشتری از دیتابیس.
    /// </summary>
    /// <param name="customerId">شناسه مشتری</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(int customerId, CancellationToken ct);
}
