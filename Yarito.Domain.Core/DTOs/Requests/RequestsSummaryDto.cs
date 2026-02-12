using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// مدل انتقال داده برای نمایش خلاصه درخواست.
/// </summary>
public class RequestsSummaryDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string WorkTitle { get; set; }
    public required string CityName { get; set; }
    public required DateTime CreateAt { get; set; }
    public required RequestStatusEnum Status { get; set; }
}
