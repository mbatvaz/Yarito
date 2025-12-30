namespace Yarito.Domain.Core.DTOs.Users
{
    public class LoginDto
    {
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required bool RememberMe { get; set; }
    }
}
