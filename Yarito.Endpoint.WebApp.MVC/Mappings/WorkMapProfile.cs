using AutoMapper;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Endpoint.WebApp.MVC.Areas.Admin.Models;

namespace Yarito.Endpoint.WebApp.MVC.Mappings
{
    public class WorkMapProfile : Profile
    {
        public WorkMapProfile()
        {
            CreateMap<WorkFormViewModel, WorkDto>();
            CreateMap<WorkDto, WorkFormViewModel>();
        }
    }
}
