namespace Yarito.Domain.Core.DTOs._Common;

/// <summary>
/// مدل انتقال داده برای آمار کلی سیستم.
/// </summary>
public class AppStatisticsDto
{
    public required int NumberOfCustomers { get; set; }
    public required int NumberOfExperts { get; set; }
    public required int NumberOfRequests { get; set; }
    public required int NumberOfBid { get; set; }
}
