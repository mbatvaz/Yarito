using Yarito.Domain.Core.DTOs.Cities;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models
{
    public class MyBidsViewModel
    {    
        // Data
        public required IReadOnlyList<BidForRequestDto> Bid { get; set; } = [];

        // Filters
        public string? Search { get; set; }
        public BidStatusEnum? Status { get; set; }

        // Paging
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
