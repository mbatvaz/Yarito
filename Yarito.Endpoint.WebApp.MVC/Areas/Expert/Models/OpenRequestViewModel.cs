using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models
{
    public class OpenRequestViewModel
    {
        public required IReadOnlyList<OpenRequestDto> OpenRequest { get; init; } = [];
        public required IReadOnlyList<WorksFullDto> ExpertWorks { get; init; } = [];

        //Filtering
        public string? Search { get; init; }
        public int? WorkId { get; init; }

        // Paging
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 6;
        public int TotalCount { get; init; }
        public int TotalPages { get; set; }
    }
}
