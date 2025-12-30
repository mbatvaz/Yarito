using Yarito.Domain.Core.DTOs._Common;
using Yarito.Domain.Core.DTOs.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public AppStatisticsDto AppStatistics { get; set; }
        public IReadOnlyList<ReviewFullDto> CommentsAwaitingApproval { get; set; } = [];
        public IReadOnlyList<RequestsSummaryDto> RequestsSummary { get; set; } = [];
    }
}
