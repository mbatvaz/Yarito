using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests
{
    public class RequestFullDto
    {
        public required int Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal ProposedPrice { get; init; }
        public required string Address { get; init; }
        public required string WorkTitle { get; init; }
        public DateTime? PreferredVisitDateTime { get; init; }
        public DateTime CreatedAt { get; init; }
        public required RequestStatusEnum Status { get; init; }
        public required int CustomerId { get; init; }
        public int? AcceptedBidId { get; init; }
        public ICollection<string> RequestImagesPath { get; set; } = [];
    }
}
