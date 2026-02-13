using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository;

/// <summary>
/// اینترفیس Query ریپازیتوری برای عملیات خواندن خدمات با Dapper.
/// </summary>
public interface IWorkQueryRepo
{
    /// <summary>
    /// گرفتن لیستی از شناسه کارها و برگرداندن لیستی از اطلاعات کامل آن‌ها.
    /// </summary>
    Task<List<WorksFullDto>> GetWorksByIDs(List<int> ids, CancellationToken ct);
}
