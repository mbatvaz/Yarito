namespace Yarito.Domain.Core.DTOs.Works;

/// <summary>
/// مدل انتقال داده برای نمایش اطلاعات دسته‌بندی با عناوین خدمات.
/// </summary>
public class CategoryStringDataDto
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public IReadOnlyCollection<string> WorksTitle { get; init; } = [];
}
