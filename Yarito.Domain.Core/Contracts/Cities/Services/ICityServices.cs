using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.Core.Contracts.Cities.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با شهرها.
/// </summary>
public interface ICityServices
{
    /// <summary>
    /// بررسی وجود شهر بر اساس شناسه.
    /// </summary>
    /// <param name="cityId">شناسه شهر</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>true در صورت وجود شهر</returns>
    Task<bool> IsExistAsync(int cityId, CancellationToken ct);

    /// <summary>
    /// دریافت لیست تمام شهرها و استان‌ها.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست شهرها با اطلاعات کامل</returns>
    Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct);
}
