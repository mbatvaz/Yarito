using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class UsersViewModel
    {
        // Data
        public IReadOnlyList<AppUserSummaryDto> UserList { get; set; } = [];

        // Filters
        public string? Search { get; set; }
        public UserTypeEnum? UserType { get; set; }

        // Paging
        public int Page { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
