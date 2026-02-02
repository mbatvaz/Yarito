using System.ComponentModel.DataAnnotations;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class AddReviewInputModel
    {
        [Required(ErrorMessage = "شناسه درخواست الزامی است.")]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "امتیاز الزامی است.")]
        [Range(1, 5, ErrorMessage = "امتیاز باید بین 1 تا 5 باشد.")]
        public int? Rating { get; set; }

        [Required(ErrorMessage = "متن نظر الزامی است.")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "نظر باید بین 5 تا 1000 کاراکتر باشد.")]
        public string? Comment { get; set; }
    }
}
