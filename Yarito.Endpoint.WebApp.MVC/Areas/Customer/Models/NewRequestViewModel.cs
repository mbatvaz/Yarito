using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class NewRequestViewModel
    {
        [ValidateNever]
        public IReadOnlyList<CategoryFullDto?> CategoryList { get; set; } = [];

        [Required(ErrorMessage = "عنوان درخواست الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان باید حداکثر 100 کاراکتر باشد.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "توضیحات باید حداکثر 1000 کاراکتر باشد.")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Range(typeof(decimal), "0", "1000000000000", ErrorMessage = "قیمت پیشنهادی نامعتبر است.")]
        [DataType(DataType.Currency)]
        public decimal? ProposedPrice { get; set; }

        [StringLength(500, ErrorMessage = "آدرس باید حداکثر 500 کاراکتر باشد.")]
        public string? Address { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? PreferredVisitDateTime { get; set; }

        [Required(ErrorMessage = "انتخاب خدمت/کار الزامی است.")]
        [Range(1, int.MaxValue, ErrorMessage = "خدمت انتخاب شده نامعتبر است.")]
        public int WorkId { get; set; }

        public bool UseProfileAddress { get; set; }

        public List<IFormFile?>? RequestImg { get; set; }
    }
}
