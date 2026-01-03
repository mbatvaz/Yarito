using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Works;

namespace Yarito.Domain.Core.DTOs.Works;

/// <summary>
/// مدل درخواست برای جستجو و فیلتر دسته‌بندی‌ها.
/// </summary>
public class CategoryReqDto : PageRequest
{
    public string? TextSearch { get; set; }

    public DateTime? From { get; init; }
    public DateTime? To { get; init; }

    public SortRequest<CategorySortableEnum>? Sort { get; init; }
}
