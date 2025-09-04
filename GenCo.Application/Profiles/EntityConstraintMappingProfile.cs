using AutoMapper;
using GenCo.Application.DTOs.EntityConstraint;
using GenCo.Application.DTOs.EntityConstraint.Requests;
using GenCo.Application.DTOs.EntityConstraint.Response;
using GenCo.Domain.Entities;
using GenCo.GenCo.Application.DTOs.EntityContraint.Requests;

namespace GenCo.Application.Profiles;

public class EntityConstraintMappingProfile : Profile
{
    public EntityConstraintMappingProfile()
    {
        // ===== EntityConstraint -> DTO =====
        CreateMap<EntityConstraint, EntityConstraintBaseDto>();

        CreateMap<EntityConstraint, EntityConstraintResponseDto>();

        CreateMap<EntityConstraint, EntityConstraintDetailDto>()
            .ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.Entity))
            .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));

        // ===== DTO -> EntityConstraint =====
        CreateMap<CreateEntityConstraintRequestDto, EntityConstraint>();
        CreateMap<UpdateEntityConstraintRequestDto, EntityConstraint>();
        CreateMap<DeleteEntityConstraintRequestDto, EntityConstraint>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}
