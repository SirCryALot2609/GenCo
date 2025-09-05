using AutoMapper;
using GenCo.Application.DTOs.FieldValidator;
using GenCo.Application.DTOs.FieldValidator.Requests;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.Profiles;

public class FieldValidatorProfile : Profile
{
    public FieldValidatorProfile()
    {
        CreateMap<CreateFieldValidatorRequestDto, FieldValidator>()
            .ForMember(dest => dest.ConfigObject, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Type = (ValidatorType)Enum.Parse(typeof(ValidatorType), src.Type, true);
                dest.ConfigJson = src.ConfigObject == null ? null : System.Text.Json.JsonSerializer.Serialize(src.ConfigObject);
            });

        CreateMap<UpdateFieldValidatorRequestDto, FieldValidator>()
            .ForMember(dest => dest.ConfigObject, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Type = (ValidatorType)Enum.Parse(typeof(ValidatorType), src.Type, true);
                dest.ConfigJson = src.ConfigObject == null ? null : System.Text.Json.JsonSerializer.Serialize(src.ConfigObject);
            });

        // Entity -> BaseDto
        CreateMap<FieldValidator, FieldValidatorBaseDto>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.ConfigJson,
                opt => opt.MapFrom(src => src.ConfigJson));

        // Entity -> ResponseDto
        CreateMap<FieldValidator, FieldValidatorResponseDto>()
            .IncludeBase<FieldValidator, FieldValidatorBaseDto>();

        // Entity -> DetailDto
        CreateMap<FieldValidator, FieldValidatorDetailDto>()
            .IncludeBase<FieldValidator, FieldValidatorBaseDto>()
            .ForMember(dest => dest.ConfigObject,
                opt => opt.MapFrom(src => src.ConfigObject))
            .ForMember(dest => dest.FieldName,
                opt => opt.MapFrom(src => src.Field.ColumnName));

    }
}
