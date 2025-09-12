using AutoMapper;
using GenCo.Application.DTOs.RelationJoinTable;
using GenCo.Application.DTOs.RelationJoinTable.Requests;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class RelationJoinTableMappingProfile : Profile
{
    public RelationJoinTableMappingProfile()
    {
        // ===== RelationJoinTable -> DTO =====
        CreateMap<RelationJoinTable, RelationJoinTableBaseDto>()
            .ForMember(dest => dest.JoinTableName, opt => opt.MapFrom(src => src.JoinTableName))
            .ForMember(dest => dest.LeftKey, opt => opt.MapFrom(src => src.LeftKey))
            .ForMember(dest => dest.RightKey, opt => opt.MapFrom(src => src.RightKey))
            .IncludeAllDerived();

        CreateMap<RelationJoinTable, RelationJoinTableResponseDto>()
            .IncludeBase<RelationJoinTable, RelationJoinTableBaseDto>();

        CreateMap<RelationJoinTable, RelationJoinTableDetailDto>()
            .IncludeBase<RelationJoinTable, RelationJoinTableBaseDto>()
            .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relation));

        // ===== DTO -> RelationJoinTable =====
        CreateMap<CreateRelationJoinTableRequestDto, RelationJoinTable>()
            .ForMember(dest => dest.RelationId, opt => opt.MapFrom(src => src.RelationId))
            .ForMember(dest => dest.JoinTableName, opt => opt.MapFrom(src => src.JoinTableName))
            .ForMember(dest => dest.LeftKey, opt => opt.MapFrom(src => src.LeftKey))
            .ForMember(dest => dest.RightKey, opt => opt.MapFrom(src => src.RightKey));

        CreateMap<UpdateRelationJoinTableRequestDto, RelationJoinTable>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RelationId, opt => opt.MapFrom(src => src.RelationId))
            .ForMember(dest => dest.JoinTableName, opt => opt.MapFrom(src => src.JoinTableName))
            .ForMember(dest => dest.LeftKey, opt => opt.MapFrom(src => src.LeftKey))
            .ForMember(dest => dest.RightKey, opt => opt.MapFrom(src => src.RightKey))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));
    }
}
