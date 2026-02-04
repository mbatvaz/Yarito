using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// DTO برای انتقال داده‌های لازم جهت ثبت یک نظر جدید.
/// </summary>
public class AddNewReviewDto
{
    public int RequestId { get; set; }
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public ReviewStatusEnum ReviewStatus { get; set; } = ReviewStatusEnum.Pending;
}
