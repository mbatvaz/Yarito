using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Models
{
    public class HomeViewModel
    {
        public required IReadOnlyList<HomeViewReviewDto> Reviews { get; set; }
        public required IReadOnlyList<CategoryStringDataDto> Categories { get; set; }
    }
}
