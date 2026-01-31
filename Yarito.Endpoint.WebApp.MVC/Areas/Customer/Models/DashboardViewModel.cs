using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class DashboardViewModel
    {
        public AppUserSummaryDto UserInfo { get; set; }
        public IReadOnlyList<RequestsSummaryDto> ActiveRequests { get; set; } = [];
    }
}
