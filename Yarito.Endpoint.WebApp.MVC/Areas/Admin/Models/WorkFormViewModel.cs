using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

public class WorkFormViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "وارد کردن عنوان سرویس الزامیست")]
    [MaxLength(100, ErrorMessage = "طول عنوان سرویس نباید بیشتر از 100 کارکتر باشد")]
    public string Title { get; init; } = string.Empty;

    [Required(ErrorMessage = "انتخاب دسته‌بندی الزامیست")]
    [Range(1, int.MaxValue, ErrorMessage = "لطفا یک دسته‌بندی انتخاب کنید")]
    public int CategoryId { get; init; }

    [Required(ErrorMessage = "وارد کردن قیمت پایه الزامیست")]
    [Range(0, double.MaxValue, ErrorMessage = "قیمت پایه نمی‌تواند منفی باشد")]
    public decimal BasePrice { get; init; }


    public IReadOnlyList<CategoryDto> Categories { get; set; } = [];

    public bool IsEditMode => Id.HasValue;
}