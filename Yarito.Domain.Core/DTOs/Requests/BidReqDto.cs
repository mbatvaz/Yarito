using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// ??? ??????? ???? ????? ? ????? ?????????.
/// </summary>
public class BidReqDto : PageRequest
{
    public BidStatusEnum? Status { get; init; }

    public int? ExpertId { get; init; }
    public int? RequestId { get; init; }

    public DateTime? From { get; init; }
    public DateTime? To { get; init; }

    public string? TextSearch { get; set; }

    public SortRequest<BidSortableEnum>? Sort { get; init; }
}
