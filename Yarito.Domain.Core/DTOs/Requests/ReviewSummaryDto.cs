using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// مدل انتقال داده برای نمایش خلاصه اطلاعات نظر.
/// </summary>
public class ReviewSummaryDto
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required int Rating { get; init; }
    public required string? Comment { get; init; }
    public required ReviewStatusEnum Status { get; init; }
}
