namespace Yarito.Domain.Core.DTOs.Works;

/// <summary>
/// مدل انتقال داده برای نمایش خدمات متخصص گروه‌بندی شده بر اساس دسته‌بندی.
/// </summary>
public class CategoryFullDto
{
    public required int Id { get; init; }
    public required string CategoryTitle { get; init; }
    public string? Description { get; init; }
    public IReadOnlyList<WorksFullDto> Works { get; init; } = [];
}
