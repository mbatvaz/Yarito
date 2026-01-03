namespace Yarito.Domain.Core.DTOs.Works;

/// <summary>
/// DTO برای ایجاد و بروزرسانی خدمات.
/// </summary>
public class WorkDto
{
    public int? Id { get; set; }
    public required string Title { get; set; }
    public decimal BasePrice { get; set; }
    public int CategoryId { get; set; }
}
