using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;