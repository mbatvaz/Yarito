using Yarito.Domain.Core.DTOs.Images;

namespace Yarito.Domain.Core.DTOs.Requests
{
    public class RequestNewDto
    {
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int WorkId { get; set; }
        public bool UseProfileAddress { get; set; }
        public string? Address { get; set; }
        public decimal? ProposedPrice { get; set; }
        public DateTime? PreferredVisitDateTime { get; set; }
        public List<ImageStreamDto> Images { get; set; } = [];
    }
}
