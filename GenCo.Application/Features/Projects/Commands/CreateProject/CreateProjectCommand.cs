using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Requests;
using GenCo.Application.DTOs.Project.Responses;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand(CreateProjectRequestDto Request)
    : IRequest<BaseResponseDto<ProjectResponseDto>>;