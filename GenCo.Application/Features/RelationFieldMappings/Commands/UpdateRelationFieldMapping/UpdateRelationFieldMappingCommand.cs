using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Requests;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.UpdateRelationFieldMapping;

public record UpdateRelationFieldMappingCommand(UpdateRelationFieldMappingRequestDto Request)
    : IRequest<BaseResponseDto<RelationFieldMappingResponseDto>>;