using AutoMapper;
using GenCo.Application.DTOs.Relation;
using GenCo.Application.DTOs.Relation.Requests;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class RelationProfile : Profile
{
    public RelationProfile()
    {
        // ========== Entity -> DTO ==========
        CreateMap<Relation, RelationBaseDto>();
        CreateMap<Relation, RelationResponseDto>();
        CreateMap<Relation, RelationDetailDto>()
            .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
            .ForMember(dest => dest.FromEntity, opt => opt.MapFrom(src => src.FromEntity))
            .ForMember(dest => dest.ToEntity, opt => opt.MapFrom(src => src.ToEntity))
            .ForMember(dest => dest.FieldMappings, opt => opt.MapFrom(src => src.FieldMappings));

        // ========== DTO -> Entity (Create) ==========
        CreateMap<CreateRelationRequestDto, Relation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())   // Id do DB generate
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

        // ========== DTO -> Entity (Update) ==========
        CreateMap<UpdateRelationRequestDto, Relation>()
            .ForMember(dest => dest.ProjectId, opt => opt.Ignore()) // Không cho update ProjectId
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());
    }
}