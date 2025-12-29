namespace Yarito.Domain.Core.DTOs.Works
{
    public class CategoryStringDataDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public IReadOnlyCollection<string> WorksTitle { get; set; } = [];
    }
}
