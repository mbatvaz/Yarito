using AutoMapper;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

namespace Yarito.Endpoint.WebApp.MVC.Mappings
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            // Users Index Page
            CreateMap<PagedResult<AppUserSummaryDto>, UsersViewModel>()
                .ForMember(d => d.Page, opt => opt.MapFrom(s => s.Page))
                .ForMember(d => d.UserList, opt => opt.MapFrom(s => s.Items))
                .ForMember(d => d.TotalCount, opt => opt.MapFrom(s => s.TotalCount))
                .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

            // Customer Details Page - Requests Paging
            CreateMap<PagedResult<RequestsSummaryDto>, CustomerDetailsViewModel>()
                .ForMember(d => d.Requests, opt => opt.MapFrom(s => s.Items))
                .ForMember(d => d.Page, opt => opt.MapFrom(s => s.Page))
                .ForMember(d => d.TotalCount, opt => opt.MapFrom(s => s.TotalCount))
                .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

            CreateMap<PagedResult<BidSummaryDto>, ExpertDetailsViewModel>()
                .ForMember(d => d.Bids, opt => opt.MapFrom(s => s.Items))
                .ForMember(d => d.Page, opt => opt.MapFrom(s => s.Page))
                .ForMember(d => d.TotalCount, opt => opt.MapFrom(s => s.TotalCount))
                .ForMember(d => d.TotalPages, opt => opt.MapFrom(s => (int)Math.Ceiling((double)s.TotalCount / s.PageSize)));

            CreateMap<UserFormViewModel, RegisterDto>()
                .ForMember(d => d.ProfileImage, opt => opt.MapFrom(s => s.ProfileImage != null ? s.ProfileImage.OpenReadStream() : null))
                .ForMember(d => d.ProfileImageUrl, opt => opt.MapFrom(s => s.ProfileImage != null ? Path.GetExtension(s.ProfileImage.FileName) : null));
        }
    }
}
