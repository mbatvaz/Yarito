using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class UserDetailsViewModel
    {
        public required int Id { get; init; }
        public required string ProfileImgPath { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required UserTypeEnum UserType { get; init; }
        public required DateTime CreatedAt { get; init; }
        public required string PhoneNumber { get; init; }
        public string? CityName { get; init; }
        public string? Email { get; init; }
        public required decimal WalletBalance { get; init; }
        public string? Address { get; init; }
    }
}
