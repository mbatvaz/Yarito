namespace Yarito.Domain.Core.DTOs.Users
{
    public class ExpertFindRequestInfoDto
    {
        public IReadOnlyList<int> WorkId { get; init; } = [];
        public int? CityId { get; init; }
    }
}
