using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class ExpertViewModel
    {
        // Page Data
        public required AppUserFullDto Info { get; init; }
        public IReadOnlyList<CategoryFullDto> Categories { get; init; } = [];
        public IReadOnlyList<ReviewFullDto> Reviews { get; init; } = [];

        // Paging
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 6;
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }
    }
}
