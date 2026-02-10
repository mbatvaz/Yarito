using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class RequestDetailsViewModel
    {
        // Page Data
        public RequestFullDto? Request { get; init; }
        public BidFullDto? AcceptedBid { get; init; }
        public ReviewSummaryDto? Review { get; init; }
        public IReadOnlyList<BidFullDto>? Bids { get; init; }

        // Add Review
        public AddReviewInputModel ReviewModel { get; init; } = new();

        // Filters
        public string? Search { get; init; }

        // Paging
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 6;
        public int TotalCount { get; init; }
        public int TotalPages { get; set; }
    }
}
