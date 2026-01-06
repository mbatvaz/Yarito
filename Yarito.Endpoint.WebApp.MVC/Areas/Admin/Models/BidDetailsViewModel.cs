using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class BidDetailsViewModel
    {
        // Data
        public required AppUserFullDto Customer { get; init; }
        public required AppUserFullDto Expert { get; init; }
        public required RequestFullDto Request { get; init; }
        public required BidFullDto Bid { get; init; }
    }
}
