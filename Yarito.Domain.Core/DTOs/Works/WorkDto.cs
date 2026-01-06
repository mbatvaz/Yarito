namespace Yarito.Domain.Core.DTOs.Works;

/// <summary>
/// DTO برای ایجاد و بروزرسانی خدمات.
/// </summary>
public class WorkDto
{
    public int? Id { get; init; }
    public string Title { get; set; }
    public decimal BasePrice { get; init; }
    public int CategoryId { get; init; }
}
