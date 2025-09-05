using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.RelationFieldMappings.Commands.SoftDeleteRelationFieldMapping;

public record SoftDeleteRelationFieldMappingCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;