using System.Text.Json;
using AutoMapper;
using GenCo.Application.DTOs.FieldValidator.Responses;
using GenCo.Domain.Entities;

namespace GenCo.Application.Resolvers;

public class FieldValidatorConfigResolver 
    : IValueResolver<FieldValidator, FieldValidatorDetailDto, FieldValidatorConfig>
{
    public FieldValidatorConfig Resolve(
        FieldValidator source, 
        FieldValidatorDetailDto destination, 
        FieldValidatorConfig destMember, 
        ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ConfigJson))
            return new FieldValidatorConfig();

        return JsonSerializer.Deserialize<FieldValidatorConfig>(source.ConfigJson) ?? new FieldValidatorConfig();
    }
}