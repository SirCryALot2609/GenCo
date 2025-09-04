using AutoMapper;
using GenCo.Application.DTOs.EntityConstraintField;
using GenCo.Application.DTOs.EntityConstraintField.Requests;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using GenCo.Domain.Entities;
using GenCo.GenCo.Application.DTOs.EntityConstraintField.Requests;

namespace GenCo.Application.Profiles;

public class EntityConstraintFieldMappingProfile : Profile
{
    public EntityConstraintFieldMappingProfile()
    {
        // ===== EntityConstraintField -> DTO =====
        CreateMap<EntityConstraintField, EntityConstraintFieldBaseDto>();
        CreateMap<EntityConstraintField, EntityConstraintFieldResponseDto>();
        CreateMap<EntityConstraintField, EntityConstraintFieldDetailDto>()
            .ForMember(dest => dest.Constraint, opt => opt.MapFrom(src => src.Constraint))
            .ForMember(dest => dest.Field, opt => opt.MapFrom(src => src.Field));

        // ===== DTO -> EntityConstraintField =====
        CreateMap<CreateEntityConstraintFieldRequestDto, EntityConstraintField>();
        CreateMap<UpdateEntityConstraintFieldRequestDto, EntityConstraintField>();
        CreateMap<DeleteEntityConstraintFieldRequestDto, EntityConstraintField>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}