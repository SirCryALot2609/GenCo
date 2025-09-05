using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Requests;
using GenCo.Application.DTOs.Project.Responses;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(UpdateProjectRequestDto Request)
    : IRequest<BaseResponseDto<ProjectResponseDto>>;