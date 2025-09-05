using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.RestoreProject;

public record RestoreProjectCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;