using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// مدل انتقال داده برای نمایش خلاصه پیشنهاد.
/// </summary>
public class BidSummaryDto
{
    public required int Id { get; init; }
    public required string ExpertFirstName { get; init; }
    public required string ExpertLastName { get; init; }
    public required string ExpertPhoneNumber { get; init; }
    public required string ServiceTitle { get; init; }
    public required decimal ProposedPrice { get; init; }
    public required BidStatusEnum Status { get; init; }
    public required DateTime ProposedVisitDate { get; init; }
}
