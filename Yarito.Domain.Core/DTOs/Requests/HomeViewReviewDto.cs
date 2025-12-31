namespace Yarito.Domain.Core.DTOs.Requests;

/// <summary>
/// مدل انتقال داده برای نمایش نظرات در صفحه اصلی.
/// </summary>
public class HomeViewReviewDto
{
    public required string FirstName { get; set; }
    public required int Rating { get; set; }
    public required string Comment { get; set; }
}
