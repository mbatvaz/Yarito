using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class CustomerDetailsViewModel
    {
        // Data
        public AppUserFullDto UserDetails { get; set; } = null!;
        public IReadOnlyList<RequestsSummaryDto> Requests { get; set; } = [];

        // Filters
        public int CustomerId { get; set; }
        public string? Search { get; set; }

        // Paging
        public int Page { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
