using AutoMapper;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

namespace Yarito.Endpoint.WebApp.MVC.Mappings
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<PagedResult<CategoryFullDto>, CategoriesViewModel>()
                .ForMember(d => d.Page, opt => opt.MapFrom(s => s.Page))
                .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.Items))
                .ForMember(d => d.TotalCount, opt => opt.MapFrom(s => s.TotalCount))
                .ForMember(d => d.PageSize, opt => opt.MapFrom(s => s.PageSize))
                .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

            CreateMap<CategoryFormViewModel, CategoryDto>();

            CreateMap<CategoryDto, CategoryFormViewModel>();
        }
    }
}
