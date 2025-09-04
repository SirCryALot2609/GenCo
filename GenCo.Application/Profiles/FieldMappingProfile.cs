using AutoMapper;
using GenCo.Application.DTOs.Field;
using GenCo.Application.DTOs.Field.Requests;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class FieldMappingProfile : Profile
{
    public FieldMappingProfile()
    {
        // ===== Field -> DTO =====
        CreateMap<Field, FieldBaseDto>();
        CreateMap<Field, FieldResponseDto>();
        CreateMap<Field, FieldDetailDto>()
            .ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.Entity))
            .ForMember(dest => dest.Validators, opt => opt.MapFrom(src => src.Validators));

        // ===== DTO -> Field =====
        CreateMap<CreateFieldRequestDto, Field>();
        CreateMap<UpdateFieldRequestDto, Field>();
        CreateMap<DeleteFieldRequestDto, Field>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}