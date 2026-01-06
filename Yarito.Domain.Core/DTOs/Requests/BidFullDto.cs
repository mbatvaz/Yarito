using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Entities.Users;
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
        public AppUserFullDto Expert { get; set; } = null!;
    }
}
