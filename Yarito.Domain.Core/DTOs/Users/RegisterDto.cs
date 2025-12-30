using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Domain.Core.DTOs.Users
{
    public class RegisterDto
    {
        public int Id { get; set; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Password { get; init; }
        public required UserTypeEnum UserType { get; init; }
    }
}
