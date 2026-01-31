using System.ComponentModel.DataAnnotations;
using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class CustomerFormViewModel
    {
        public IReadOnlyList<CityFullDto?> CityList { get; set; } = [];
        public string? PhoneNumber { get; set; }
        public string? CurrentProfileImagePath { get; set; }


        [Required(ErrorMessage = "وارد کردن نام الزامیست")]
        [MaxLength(50, ErrorMessage = "طول نام نباید بیشتر از 50 کاراکتر باشد")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامیست")]
        [MaxLength(50, ErrorMessage = "طول نام خانوادگی نباید بیشتر از 50 کاراکتر باشد")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست.")]
        public string? Email { get; set; }

        [MaxLength(500, ErrorMessage = "آدرس حداکثر ۵۰۰ کاراکتر می‌تواند باشد.")]
        public string? Address { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "شهر انتخاب شده معتبر نیست.")]
        public int? CityId { get; set; }

        public IFormFile? ProfileImage { get; set; }
        public bool DeleteProfileImage { get; set; }
    }
}