namespace Yarito.Domain.Core.DTOs.Works;

/// <summary>
/// مدل انتقال داده برای ایجاد یا ویرایش دسته‌بندی.
/// </summary>
public class CategoryDto
{
    public int? Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
}
