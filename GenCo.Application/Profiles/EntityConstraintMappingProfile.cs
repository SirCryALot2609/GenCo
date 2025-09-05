using AutoMapper;
using GenCo.Application.DTOs.EntityConstraint;
using GenCo.Application.DTOs.EntityConstraint.Requests;
using GenCo.Application.DTOs.EntityConstraint.Response;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.Profiles;

public class EntityConstraintMappingProfile : Profile
{
    public EntityConstraintMappingProfile()
    {
        // ===== EntityConstraint -> DTO =====
        CreateMap<EntityConstraint, EntityConstraintBaseDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<EntityConstraint, EntityConstraintResponseDto>();

        CreateMap<EntityConstraint, EntityConstraintDetailDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));

        // ===== DTO -> EntityConstraint =====
        CreateMap<CreateEntityConstraintRequestDto, EntityConstraint>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ConstraintType>(src.Type)))
            .ForMember(dest => dest.Fields, opt => opt.Ignore()); // xử lý FieldIds riêng trong handler

        CreateMap<UpdateEntityConstraintRequestDto, EntityConstraint>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ConstraintType>(src.Type)))
            .ForMember(dest => dest.Fields, opt => opt.Ignore());
    }
}