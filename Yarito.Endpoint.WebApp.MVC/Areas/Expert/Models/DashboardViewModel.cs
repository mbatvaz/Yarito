using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models
{
    public class DashboardViewModel
    {
        public required UserDashboardDto UserInfo { get; init; }
        public required IReadOnlyList<BidForRequestDto> TodayVisits { get; init; } = [];
        public required IReadOnlyList<CategoryFullDto> ExpertWorks { get; init; } = [];


        // Paging
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 6;
        public int TotalCount { get; init; }
        public int TotalPages { get; set; }
    }
}
