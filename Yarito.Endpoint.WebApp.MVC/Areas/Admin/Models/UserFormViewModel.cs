using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Yarito.Domain.Core.DTOs.Cities;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class UserFormViewModel
    {
        //Send from controller
        public IReadOnlyList<CityFullDto?> CityList { get; set; } = [];

        //Receive from user
        [Required(ErrorMessage = "وارد کردن نام الزامیست")]
        [MaxLength(50, ErrorMessage = "طول نام نباید بیشتر از 50 کاراکتر باشد")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامیست")]
        [MaxLength(50, ErrorMessage = "طول نام خانوادگی نباید بیشتر از 50 کاراکتر باشد")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "شماره موبایل الزامی است.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره موبایل باید دقیقاً 11 رقم باشد.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل باید با 09 شروع شود و 11 رقم باشد.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد.")]
        [PasswordPropertyText]
        public string? Password { get; set; }

        [Required(ErrorMessage = "نقش خود را به درستی انتخاب کنید")]
        public UserTypeEnum? UserType { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست.")]
        public string? Email { get; init; }

        [Range(0, double.MaxValue, ErrorMessage = "موجودی پایه باید بزرگتر یا مساوی صفر باشد.")]
        public decimal BaseWalletBalance { get; init; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "شهر انتخاب شده معتبر نیست.")]
        public int? CityId { get; init; }

        [MaxLength(500, ErrorMessage = "آدرس حداکثر ۵۰۰ کاراکتر می‌تواند باشد.")]
        public string? Address { get; init; }

        public IFormFile? ProfileImage { get; init; }
    }
}
