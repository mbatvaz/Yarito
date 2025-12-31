using Yarito.Domain.Core.Enums._Common;

namespace Yarito.Domain.Core.Entities._Common;

/// <summary>
/// کلاس جنریک برای تنظیمات مرتب‌سازی.
/// </summary>
/// <typeparam name="T">نوع enum فیلدهای قابل مرتب‌سازی</typeparam>
public class SortRequest<T>
{
    public T? SortBy { get; init; }
    public SortDirectionEnum Direction { get; init; } = SortDirectionEnum.Ascending;
}
