using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class CategoriesViewModel
    {
        // Data
        public IReadOnlyList<CategoryFullDto> Categories { get; set; } = [];

        // Filters
        public string? Search { get; set; }

        // Paging
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
