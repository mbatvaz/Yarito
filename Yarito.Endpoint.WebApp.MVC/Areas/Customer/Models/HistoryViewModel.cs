using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class HistoryViewModel
    {
        // Page Data
        public IReadOnlyList<RequestsSummaryDto> Requests { get; set; } = [];

        // Filters
        public string? Search { get; init; }
        public RequestStatusEnum? Status { get; init; }

        // Paging
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 6;
        public int TotalCount { get; init; }
        public int TotalPages { get; set; }
    }
}
