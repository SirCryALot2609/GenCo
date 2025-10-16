using AutoMapper;
using GenCo.Application.DTOs.Entity;
using GenCo.Application.DTOs.Entity.Requests;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class EntityMappingProfile : Profile
{
    public EntityMappingProfile()
    {
        // ===== Entity -> DTO =====
        CreateMap<Entity, EntityBaseDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
            .IncludeAllDerived();

        CreateMap<Entity, EntityResponseDto>()
            .IncludeBase<Entity, EntityBaseDto>();

        CreateMap<Entity, EntityDetailDto>()
            .IncludeBase<Entity, EntityBaseDto>()
            .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
            .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields))
            .ForMember(dest => dest.FromRelations, opt => opt.MapFrom(src => src.FromRelations))
            .ForMember(dest => dest.ToRelations, opt => opt.MapFrom(src => src.ToRelations))
            .ForMember(dest => dest.Constraints, opt => opt.MapFrom(src => src.Constraints));

        // ===== DTO -> Entity =====
        CreateMap<CreateEntityRequestDto, Entity>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label));

        CreateMap<UpdateEntityRequestDto, Entity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));
    }
}