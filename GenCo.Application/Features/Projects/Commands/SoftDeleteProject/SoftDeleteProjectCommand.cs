using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.SoftDeleteProject;
public record SoftDeleteProjectCommand(Guid Id)
    : IRequest<BaseResponseDto<bool>>;
