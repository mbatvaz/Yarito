using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

public class ExpertDetailsViewModel
{
    // Data
    public required AppUserFullDto UserDetails { get; init; }
    public required IReadOnlyList<CategoryFullDto> ExpertWorks { get; init; } = [];
    public IReadOnlyList<BidSummaryDto> Bids { get; init; } = [];

    // Filters
    public int ExpertId { get; set; }
    public string? Search { get; set; }

    // Paging
    public int Page { get; set; } = 1;
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
