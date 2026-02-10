using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests
{
    public class BidForRequestDto
    {
        public required int BidId { get; init; }
        public required int RequestId { get; init; }
        public required string RequestTitle { get; init;}
        public required string CustomerFirstName { get; init; }
        public required string CustomerLastName {get; init; }
        public required string CustomerPhoneNumber { get; init; }
        public required string WorkTitle { get; init; }
        public required DateTime BidCreatedAt { get; init; }
        public decimal ProposedPrice { get; init; }
        public required BidStatusEnum Status { get; init; }
    }
}
