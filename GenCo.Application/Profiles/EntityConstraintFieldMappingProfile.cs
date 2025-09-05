using AutoMapper;
using GenCo.Application.DTOs.EntityConstraintField;
using GenCo.Application.DTOs.EntityConstraintField.Requests;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class EntityConstraintFieldMappingProfile : Profile
{
    public EntityConstraintFieldMappingProfile()
    {
        // ===== EntityConstraintField -> DTO =====
        CreateMap<EntityConstraintField, EntityConstraintFieldBaseDto>()
            .ForMember(dest => dest.ConstraintId, opt => opt.MapFrom(src => src.ConstraintId))
            .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.FieldId));

        CreateMap<EntityConstraintField, EntityConstraintFieldResponseDto>()
            .IncludeBase<EntityConstraintField, EntityConstraintFieldBaseDto>();

        CreateMap<EntityConstraintField, EntityConstraintFieldDetailDto>()
            .IncludeBase<EntityConstraintField, EntityConstraintFieldBaseDto>()
            .ForMember(dest => dest.ConstraintName, opt => opt.MapFrom(src => src.Constraint.ConstraintName))
            .ForMember(dest => dest.FieldName, opt => opt.MapFrom(src => src.Field.ColumnName));

        // ===== DTO -> EntityConstraintField =====
        CreateMap<CreateEntityConstraintFieldRequestDto, EntityConstraintField>()
            .ForMember(dest => dest.ConstraintId, opt => opt.MapFrom(src => src.ConstraintId))
            .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.FieldId));

        CreateMap<UpdateEntityConstraintFieldRequestDto, EntityConstraintField>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ConstraintId, opt => opt.MapFrom(src => src.ConstraintId))
            .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.FieldId))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));
    }
}
