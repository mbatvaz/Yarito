using System.ComponentModel.DataAnnotations;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Account.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "شماره موبایل الزامی است.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره موبایل باید دقیقاً 11 رقم باشد.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل باید با 09 شروع شود و 11 رقم باشد.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد.")]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
