namespace Yarito.Domain.Core.DTOs.Requests
{
    public class HomePageReviewDto
    {
        public required string FirstName { get; set; }
        public required int Rating { get; set; }
        public required string Comment { get; set; }
    }
}
