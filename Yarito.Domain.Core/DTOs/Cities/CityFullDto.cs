namespace Yarito.Domain.Core.DTOs.Cities
{
    public class CityFullDto
    {
        public int Id { get; set; }
        public required string Name { get; init; }
        public string? ParentName { get; init; }
    }
}
