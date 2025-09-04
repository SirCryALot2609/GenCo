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
        CreateMap<Entity, EntityBaseDto>();

        CreateMap<Entity, EntityResponseDto>();

        CreateMap<Entity, EntityDetailDto>()
            .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
            .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields))
            .ForMember(dest => dest.FromRelations, opt => opt.MapFrom(src => src.FromRelations))
            .ForMember(dest => dest.ToRelations, opt => opt.MapFrom(src => src.ToRelations));
            // .ForMember(dest => dest.Constraints, opt => opt.MapFrom(src => src.Constraints));

        // ===== DTO -> Entity =====
        CreateMap<CreateEntityRequestDto, Entity>();
        CreateMap<UpdateEntityRequestDto, Entity>();
        CreateMap<DeleteEntityRequestDto, Entity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}