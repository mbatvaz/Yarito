using System.ComponentModel.DataAnnotations;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models
{
    public class CategoryFormViewModel
    {
        public int? Id { get; init; }

        [Required(ErrorMessage = "وارد کردن عنوان دسته بندی الزامیست")]
        [MaxLength(100, ErrorMessage = "طول عنوان دسته بندی نباید بیشتر از 100 کاراکتر باشد")]
        public string Title { get; init; } = string.Empty;

        [MaxLength(500, ErrorMessage = "طول توضیحات دسته بندی نباید بیشتر از 500 کاراکتر باشد")]
        public string? Description { get; init; }

        public bool IsEditMode => Id.HasValue;
    }
}
