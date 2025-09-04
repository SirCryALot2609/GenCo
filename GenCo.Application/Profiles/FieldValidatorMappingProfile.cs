using AutoMapper;
using GenCo.Application.DTOs.FieldValidator;
using GenCo.Application.DTOs.FieldValidator.Requests;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Application.Resolvers;
using GenCo.Domain.Entities;

namespace GenCo.Application.Profiles;

public class FieldValidatorMappingProfile : Profile
{
    public class FieldValidatorProfile : Profile
    {
        public FieldValidatorProfile()
        {
            CreateMap<FieldValidator, FieldValidatorBaseDto>();
            CreateMap<FieldValidator, FieldValidatorResponseDto>();

            CreateMap<FieldValidator, FieldValidatorDetailDto>()
                .ForMember(dest => dest.Field, opt => opt.MapFrom(src => src.Field))
                .ForMember(dest => dest.ConfigObject, opt => opt.MapFrom<FieldValidatorConfigResolver>());

            CreateMap<CreateFieldValidatorRequestDto, FieldValidator>();
            CreateMap<UpdateFieldValidatorRequestDto, FieldValidator>();
            CreateMap<DeleteFieldValidatorRequestDto, FieldValidator>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}