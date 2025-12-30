using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests
{
    public class ReviewReqDto : PageRequest
    {
        public ReviewStatusEnum? ApprovalStatus { get; init; }

        public int? Rating { get; init; }
        public int? MinRating { get; init; }
        public int? MaxRating { get; init; }

        public int? ExpertId { get; init; }
        public int? CustomerId { get; init; }
        public int? RequestId { get; init; }

        public DateTime? From { get; init; }
        public DateTime? To { get; init; }

        public string? TextSearch { get; set; }

        public SortRequest<ReviewSortableEnum>? Sort { get; init; }
    }
}
