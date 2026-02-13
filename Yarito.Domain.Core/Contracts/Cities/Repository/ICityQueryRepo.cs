using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Domain.Core.Contracts.Cities.Repository;

/// <summary>
/// اینترفیس Query ریپازیتوری برای عملیات خواندن شهرها با Dapper.
/// </summary>
public interface ICityQueryRepo
{
    /// <summary>
    /// دریافت لیست تمام شهرها.
    /// </summary>
    Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct);
}
