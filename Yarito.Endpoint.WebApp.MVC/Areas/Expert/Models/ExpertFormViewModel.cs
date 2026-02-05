using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Yarito.Domain.Core.DTOs.Cities;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models;

public class ExpertFormViewModel
{
    public IReadOnlyList<CityFullDto?>? CityList { get; set; }
    public IReadOnlyList<CategoryFullDto>? CategoryList { get; set; }
    public IReadOnlyList<CategoryFullDto>? CurrentWorkGrouped { get; set; }

    [Required(ErrorMessage = "وارد کردن نام الزامیست")]
    [MaxLength(50, ErrorMessage = "طول نام نباید بیشتر از 50 کاراکتر باشد")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامیست")]
    [MaxLength(50, ErrorMessage = "طول نام خانوادگی نباید بیشتر از 50 کاراکتر باشد")]
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است")]
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
    public int? CityId { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public string? CurrentProfileImagePath { get; set; }
    public bool DeleteProfileImage { get; set; }
    public List<int> WorkIds { get; set; } = [];
}
