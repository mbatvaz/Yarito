using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class ReviewsViewModel
    {
        //Data
        public IReadOnlyList<ReviewFullDto> Reviews { get; set; } = [];

        // Filters
        public ReviewStatusEnum? Status { get; set; }
        public string? Search { get; set; }

        // Paging
        public int Page { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
