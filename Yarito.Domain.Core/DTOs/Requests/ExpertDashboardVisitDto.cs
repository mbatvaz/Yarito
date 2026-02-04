namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// مدل انتقال داده برای نمایش مراجعات امروز در داشبورد متخصص.
/// </summary>
public class ExpertDashboardVisitDto
{
    public required int Id { get; init; }
    public required int BidId { get; init; }
    public required string Title { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string CustomerPhoneNumber { get; init; }
    public required DateTime VisitDateTime { get; init; }
}
