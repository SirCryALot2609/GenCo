using AutoMapper;
using GenCo.Application.DTOs.RelationFieldMapping;
using GenCo.Application.DTOs.RelationFieldMapping.Requests;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class RelationFieldMappingProfile : Profile
{
    public RelationFieldMappingProfile()
    {
        // ===== RelationFieldMapping -> DTO =====
        CreateMap<RelationFieldMapping, RelationFieldMappingBaseDto>()
            .ForMember(dest => dest.RelationId, opt => opt.MapFrom(src => src.RelationId))
            .ForMember(dest => dest.FromFieldId, opt => opt.MapFrom(src => src.FromFieldId))
            .ForMember(dest => dest.ToFieldId, opt => opt.MapFrom(src => src.ToFieldId))
            .IncludeAllDerived();

        CreateMap<RelationFieldMapping, RelationFieldMappingResponseDto>()
            .IncludeBase<RelationFieldMapping, RelationFieldMappingBaseDto>();

        CreateMap<RelationFieldMapping, RelationFieldMappingDetailDto>()
            .IncludeBase<RelationFieldMapping, RelationFieldMappingBaseDto>()
            .ForMember(dest => dest.FromField, opt => opt.MapFrom(src => src.FromField))
            .ForMember(dest => dest.ToField, opt => opt.MapFrom(src => src.ToField))
            .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relation));

        // ===== DTO -> RelationFieldMapping =====
        CreateMap<CreateRelationFieldMappingRequestDto, RelationFieldMapping>()
            .ForMember(dest => dest.RelationId, opt => opt.MapFrom(src => src.RelationId))
            .ForMember(dest => dest.FromFieldId, opt => opt.MapFrom(src => src.FromFieldId))
            .ForMember(dest => dest.ToFieldId, opt => opt.MapFrom(src => src.ToFieldId));

        CreateMap<UpdateRelationFieldMappingRequestDto, RelationFieldMapping>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RelationId, opt => opt.MapFrom(src => src.RelationId))
            .ForMember(dest => dest.FromFieldId, opt => opt.MapFrom(src => src.FromFieldId))
            .ForMember(dest => dest.ToFieldId, opt => opt.MapFrom(src => src.ToFieldId))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));
    }
}
