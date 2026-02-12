namespace Yarito.Domain.Core.DTOs.Requests
{
    public class OpenRequestDto
    {
        public required int RequestId { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal? ProposedPrice { get; init; }
        public required string WorkTitle { get; init; }
        public DateTime? PreferredVisitDateTime { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
