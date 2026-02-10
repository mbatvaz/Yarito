using System.ComponentModel.DataAnnotations;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Customer.Models
{
    public class AddReviewInputModel
    {

        public int RequestId { get; set; }

        [Required(ErrorMessage = "امتیاز الزامی است.")]
        [Range(1, 5, ErrorMessage = "امتیاز باید بین 1 تا 5 باشد.")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "نظر نمی‌تواند بیشتر از 1000 کاراکتر باشد.")]
        public string? Comment { get; set; }
    }
}
