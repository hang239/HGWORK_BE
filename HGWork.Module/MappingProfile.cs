using AutoMapper;
using HGWork.DTO;
using HGWork.Model;

namespace HGWork.Module
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ProjectDto, Project>();
        }
    }
}