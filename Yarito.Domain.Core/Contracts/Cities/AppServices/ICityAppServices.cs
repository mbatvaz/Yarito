using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.Core.Contracts.Cities.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با شهرها در لایه نمایش.
/// </summary>
public interface ICityAppServices
{
    /// <summary>
    /// دریافت لیست تمام شهرها و استان‌ها.
    /// </summary>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>لیست شهرها با اطلاعات کامل</returns>
    Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct);
}
