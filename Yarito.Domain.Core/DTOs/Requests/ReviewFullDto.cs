using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// مدل انتقال داده برای نمایش اطلاعات کامل نظر.
/// </summary>
public class ReviewFullDto
{
    public int ReviewId { get; set; }
    public int? BidId { get; set; }
    public int RequestId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public required int Rating { get; set; }
    public DateTime CreateAt { get; set; }
    public string? Comment { get; set; }
    public ReviewStatusEnum ReviewStatus { get; set; }
}
