namespace Yarito.Domain.Core.DTOs.Users
{
    public class UserHeaderInfoDto
    {
        public required int UserId { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string ProfileImagePath { get; init; }
    }
}
