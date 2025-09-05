using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.Relations.Commands.DeleteRelation;
public record DeleteRelationCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
