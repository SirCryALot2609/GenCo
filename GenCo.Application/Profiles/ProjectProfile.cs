using AutoMapper;
using GenCo.Application.DTOs.Project;
using GenCo.Application.DTOs.Project.Requests;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        // ===== Entity -> DTO =====
        CreateMap<Project, ProjectBaseDto>();
        CreateMap<Project, ProjectResponseDto>();
        CreateMap<Project, ProjectListItemDto>();
        CreateMap<Project, ProjectDetailDto>()
            .ForMember(dest => dest.Entities, opt => opt.MapFrom(src => src.Entities))
            .ForMember(dest => dest.Relations, opt => opt.MapFrom(src => src.Relations));
            // .ForMember(dest => dest.Workflows, opt => opt.MapFrom(src => src.Workflows))
            // .ForMember(dest => dest.UIConfigs, opt => opt.MapFrom(src => src.UIConfigs))
            // .ForMember(dest => dest.ServiceConfigs, opt => opt.MapFrom(src => src.ServiceConfigs));

        // ===== DTO -> Entity =====
        CreateMap<CreateProjectRequestDto, Project>();
        CreateMap<UpdateProjectRequestDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<DeleteProjectRequestDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}
