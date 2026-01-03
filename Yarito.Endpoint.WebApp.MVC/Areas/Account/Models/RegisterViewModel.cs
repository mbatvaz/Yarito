using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Account.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام الزامیست")]
        [MaxLength(50, ErrorMessage = "طول نام نباید بیشتر از 50 کاراکتر باشد")]
        public required string FirstName { get; init; }

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامیست")]
        [MaxLength(50, ErrorMessage = "طول نام خانوادگی نباید بیشتر از 50 کاراکتر باشد")]
        public required string LastName { get; init; }

        [Required(ErrorMessage = "شماره موبایل الزامی است.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره موبایل باید دقیقاً 11 رقم باشد.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل باید با 09 شروع شود و 11 رقم باشد.")]
        public required string PhoneNumber { get; init; }

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد.")]
        [PasswordPropertyText]
        public required string Password { get; init; }

        [Required(ErrorMessage = "نقش خود را به درستی انتخاب کنید")]
        public required UserTypeEnum UserType { get; init; }
    }
}
