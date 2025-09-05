using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using MediatR;

namespace GenCo.Application.Features.Projects.Queries.GetProjectById;
public record GetProjectByIdQuery(Guid Id, bool IncludeDetails = false)
    : IRequest<BaseResponseDto<ProjectDetailDto>>;
