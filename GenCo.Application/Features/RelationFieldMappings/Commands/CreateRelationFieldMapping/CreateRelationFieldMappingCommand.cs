using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationFieldMapping.Requests;
using GenCo.Application.DTOs.RelationFieldMapping.Responses;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.CreateRelationFieldMapping;

public record CreateRelationFieldMappingCommand(CreateRelationFieldMappingRequestDto Request)
    : IRequest<BaseResponseDto<RelationFieldMappingResponseDto>>;