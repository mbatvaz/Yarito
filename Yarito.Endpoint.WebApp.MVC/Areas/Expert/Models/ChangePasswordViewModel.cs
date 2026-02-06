using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "رمز عبور کنونی الزامی است.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "رمز عبور کنونی باید حداقل ۸ کاراکتر باشد.")]
        public required string OldPassword { get; init; } = null!;

        [Required(ErrorMessage = "رمز عبور جدید الزامی است.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "رمز عبور جدید باید حداقل ۸ کاراکتر باشد.")]
        public required string NewPassword { get; init; } = null!;

        [Required(ErrorMessage = "تکرار رمز عبور جدید الزامی است.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "رمز عبور جدید و تکرار آن یکسان نیستند.")]
        public required string ConfirmNewPassword { get; init; } = null!;
    }
}
