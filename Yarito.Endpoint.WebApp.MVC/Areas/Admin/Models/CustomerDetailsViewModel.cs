using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class CustomerDetailsViewModel
    {
        // Data
        public required AppUserFullDto Customer { get; init; }
        public IReadOnlyList<RequestsSummaryDto> Requests { get; init; } = [];

        // Filters
        public int CustomerId { get; set; }
        public string? Search { get; set; }

        // Paging
        public int Page { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
