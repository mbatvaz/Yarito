namespace Yarito.Domain.Core.DTOs._Common
{
    public class AppStatisticsDto
    {
        public required int NumberOfCustomers { get; set; }
        public required int NumberOfExperts { get; set; }
        public required int NumberOfRequests { get; set; }
        public required int NumberOfBid { get; set; }
    }
}
