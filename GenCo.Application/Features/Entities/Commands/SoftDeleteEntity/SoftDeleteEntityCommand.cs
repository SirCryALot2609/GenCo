using GenCo.Application.DTOs.Common;
using MediatR;
namespace GenCo.Application.Features.Entities.Commands.SoftDeleteEntity;
public record SoftDeleteEntityCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;