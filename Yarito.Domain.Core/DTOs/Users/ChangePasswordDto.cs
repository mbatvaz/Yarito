using System.ComponentModel.DataAnnotations;

namespace Yarito.Domain.Core.DTOs.Users
{
    public class ChangePasswordDto
    {
        public required string OldPassword { get; init; }
        public required string NewPassword { get; init; }
        public required int Id { get; init; }
    }
}
