namespace Yarito.Domain.Core.Entities._Common;

/// <summary>
/// کلاس جنریک برای نتایج صفحه‌بندی شده.
/// </summary>
/// <typeparam name="T">نوع آیتم‌های نتیجه</typeparam>
public class PagedResult<T>
{
    public required IReadOnlyList<T> Items { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required int TotalCount { get; init; }

    public bool HasNext => Page < (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPrev => Page > 1;
}
