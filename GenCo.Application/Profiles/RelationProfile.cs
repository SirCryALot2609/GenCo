using AutoMapper;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.DTOs.Relation.Requests;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class RelationMappingProfile : Profile
{
    public RelationMappingProfile()
    {
        // ===== Relation -> DTO =====
        CreateMap<Relation, RelationBaseDto>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.FromEntityId, opt => opt.MapFrom(src => src.FromEntityId))
            .ForMember(dest => dest.ToEntityId, opt => opt.MapFrom(src => src.ToEntityId))
            .ForMember(dest => dest.RelationType, opt => opt.MapFrom(src => src.RelationType))
            .ForMember(dest => dest.OnDelete, opt => opt.MapFrom(src => src.OnDelete))
            .ForMember(dest => dest.RelationName, opt => opt.MapFrom(src => src.RelationName))
            .IncludeAllDerived();

        CreateMap<Relation, RelationResponseDto>()
            .IncludeBase<Relation, RelationBaseDto>();

        CreateMap<Relation, RelationDetailDto>()
            .IncludeBase<Relation, RelationBaseDto>()
            .ForMember(dest => dest.FromEntity, opt => opt.MapFrom(src => src.FromEntity))
            .ForMember(dest => dest.ToEntity, opt => opt.MapFrom(src => src.ToEntity))
            .ForMember(dest => dest.FieldMappings, opt => opt.MapFrom(src => src.FieldMappings));
            // .ForMember(dest => dest.JoinTables, opt => opt.MapFrom(src => src.JoinTables));

        // ===== DTO -> Relation =====
        CreateMap<CreateRelationRequestDto, Relation>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.FromEntityId, opt => opt.MapFrom(src => src.FromEntityId))
            .ForMember(dest => dest.ToEntityId, opt => opt.MapFrom(src => src.ToEntityId))
            .ForMember(dest => dest.RelationType, opt => opt.MapFrom(src => src.RelationType))
            .ForMember(dest => dest.OnDelete, opt => opt.MapFrom(src => src.OnDelete))
            .ForMember(dest => dest.RelationName, opt => opt.MapFrom(src => src.RelationName));

        CreateMap<UpdateRelationRequestDto, Relation>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.FromEntityId, opt => opt.MapFrom(src => src.FromEntityId))
            .ForMember(dest => dest.ToEntityId, opt => opt.MapFrom(src => src.ToEntityId))
            .ForMember(dest => dest.RelationType, opt => opt.MapFrom(src => src.RelationType))
            .ForMember(dest => dest.OnDelete, opt => opt.MapFrom(src => src.OnDelete))
            .ForMember(dest => dest.RelationName, opt => opt.MapFrom(src => src.RelationName))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));
    }
}