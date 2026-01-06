using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class RequestDetailsViewModel
    {
        // Data
        public required AppUserFullDto CustomerInfo { get; init; }
        public required RequestFullDto RequestInfo { get; init; }
        public required IReadOnlyList<string> RequestImagesPath { get; init; } = [];
        public required IReadOnlyList<BidSummaryDto> BidList { get; init; } = [];
        public BidSummaryDto? AcceptedBid { get; init; }

        // Filters
        public string? Search { get; set; }

        // Paging
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }
}
