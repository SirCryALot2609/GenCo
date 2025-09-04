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
        // ====== Entity -> DTO ======
        CreateMap<RelationFieldMapping, RelationFieldMappingBaseDto>();
        CreateMap<RelationFieldMapping, RelationFieldMappingResponseDto>();
        CreateMap<RelationFieldMapping, RelationFieldMappingDetailDto>()
            .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relation))
            .ForMember(dest => dest.FromField, opt => opt.MapFrom(src => src.FromField))
            .ForMember(dest => dest.ToField, opt => opt.MapFrom(src => src.ToField));

        // ====== DTO -> Entity ======
        CreateMap<CreateRelationFieldMappingRequestDto, RelationFieldMapping>();
        CreateMap<UpdateRelationFieldMappingRequestDto, RelationFieldMapping>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<DeleteRelationFieldMappingRequestDto, RelationFieldMapping>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}