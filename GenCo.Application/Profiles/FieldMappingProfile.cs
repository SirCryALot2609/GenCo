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
        CreateMap<Field, FieldBaseDto>()
            .ForMember(dest => dest.ColumnName, opt => opt.MapFrom(src => src.ColumnName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
            .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.Scale))
            .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.IsRequired))
            .ForMember(dest => dest.IsAutoIncrement, opt => opt.MapFrom(src => src.IsAutoIncrement))
            .ForMember(dest => dest.DefaultValue, opt => opt.MapFrom(src => src.DefaultValue))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.ColumnOrder, opt => opt.MapFrom(src => src.ColumnOrder));

        CreateMap<Field, FieldResponseDto>()
            .IncludeBase<Field, FieldBaseDto>();

        CreateMap<Field, FieldDetailDto>()
            .IncludeBase<Field, FieldBaseDto>()
            .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId))
            .ForMember(dest => dest.Validators, opt => opt.MapFrom(src => src.Validators));

        // ===== DTO -> Field =====
        CreateMap<CreateFieldRequestDto, Field>()
            .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId))
            .ForMember(dest => dest.ColumnName, opt => opt.MapFrom(src => src.ColumnName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
            .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.Scale))
            .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.IsRequired))
            .ForMember(dest => dest.IsAutoIncrement, opt => opt.MapFrom(src => src.IsAutoIncrement))
            .ForMember(dest => dest.DefaultValue, opt => opt.MapFrom(src => src.DefaultValue))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.ColumnOrder, opt => opt.MapFrom(src => src.ColumnOrder));

        CreateMap<UpdateFieldRequestDto, Field>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId))
            .ForMember(dest => dest.ColumnName, opt => opt.MapFrom(src => src.ColumnName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
            .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.Scale))
            .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.IsRequired))
            .ForMember(dest => dest.IsAutoIncrement, opt => opt.MapFrom(src => src.IsAutoIncrement))
            .ForMember(dest => dest.DefaultValue, opt => opt.MapFrom(src => src.DefaultValue))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.ColumnOrder, opt => opt.MapFrom(src => src.ColumnOrder))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));
    }
}