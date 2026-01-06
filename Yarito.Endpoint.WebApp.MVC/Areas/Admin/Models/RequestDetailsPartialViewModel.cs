using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class RequestDetailsPartialViewModel
    {
        public required int Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal ProposedPrice { get; init; }
        public required string Address { get; init; }
        public DateTime? PreferredVisitDateTime { get; init; }
        public DateTime CreatedAt { get; init; }
        public required RequestStatusEnum Status { get; init; }
    }
}
