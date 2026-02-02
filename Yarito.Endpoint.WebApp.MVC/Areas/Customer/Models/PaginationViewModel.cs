namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class PaginationViewModel
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public string? Area { get; set; } = null;
        public string Controller { get; set; } = "";
        public string Action { get; set; } = "";


        public Dictionary<string, string?> RouteValues { get; set; } = new();
    }
}
