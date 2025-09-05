using AutoMapper;
using GenCo.Application.DTOs.Project;
using GenCo.Application.DTOs.Project.Requests;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        // ===== Project -> DTO =====
        CreateMap<Project, ProjectBaseDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Project, ProjectResponseDto>()
            .IncludeBase<Project, ProjectBaseDto>();

        CreateMap<Project, ProjectDetailDto>()
            .IncludeBase<Project, ProjectBaseDto>()
            .ForMember(dest => dest.Entities, opt => opt.MapFrom(src => src.Entities))
            .ForMember(dest => dest.Relations, opt => opt.MapFrom(src => src.Relations));
        // .ForMember(dest => dest.Workflows, opt => opt.MapFrom(src => src.Workflows))
        // .ForMember(dest => dest.UiConfigs, opt => opt.MapFrom(src => src.UiConfigs))
        // .ForMember(dest => dest.ServiceConfigs, opt => opt.MapFrom(src => src.ServiceConfigs))
        // .ForMember(dest => dest.Connections, opt => opt.MapFrom(src => src.Connections));

        // ===== DTO -> Project =====
        CreateMap<CreateProjectRequestDto, Project>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<UpdateProjectRequestDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));
    }
}