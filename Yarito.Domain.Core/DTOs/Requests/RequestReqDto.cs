using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// مدل درخواست برای جستجو و فیلتر درخواست‌های خدماتی.
/// </summary>
public class RequestReqDto : PageRequest
{
    public RequestStatusEnum? FirstStatus { get; init; }
    public RequestStatusEnum? SecondStatus { get; init; }

    public decimal? MinProposedPrice { get; init; }
    public decimal? MaxProposedPrice { get; init; }

    public int? CustomerId { get; init; }
    public int? ExpertId { get; init; }
    public int? WorkId { get; init; }
    public int? CityId { get; init; }

    public DateTime? From { get; init; }
    public DateTime? To { get; init; }

    public DateTime? PreferredFrom { get; init; }
    public DateTime? PreferredTo { get; init; }

    public string? TextSearch { get; set; }

    public SortRequest<RequestSortableEnum>? Sort { get; init; }
}
