using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.DeleteRelationFieldMapping;

public record DeleteRelationFieldMappingCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;