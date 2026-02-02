using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests
{
    public class BidFullDto
    {
        public required int Id { get; init; }
        public string? Description { get; init; }
        public required decimal ProposedPrice { get; init; }
        public required DateTime ProposedVisitDateTime { get; init; }
        public required DateTime CreatedAt { get; init; }
        public BidStatusEnum Status { get; set; } = BidStatusEnum.Pending;
        public required int ExpertId { get; init; }
        public required int RequestId { get; init; }
    }
}
