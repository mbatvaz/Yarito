using Yarito.Domain.Core.DTOs.Cities;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

public class RequestsViewModel
{
    // Data
    public IReadOnlyList<RequestCardDto> Requests { get; set; } = [];
    public IReadOnlyList<CityFullDto> Cities { get; set; } = [];

    // Filters
    public string? Search { get; set; }
    public RequestStatusEnum? Status { get; set; }
    public int? CityId { get; set; }

    // Paging
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
