using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.RestoreRelationFieldMapping;

public record RestoreRelationFieldMappingCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;