using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project;
using MediatR;

namespace GenCo.Application.Features.Projects.Queries.GetProjectList;

public record GetProjectListQuery(
    string? Keyword = null,
    int PageNumber = 1,
    int PageSize = 10,
    bool IncludeAllCollections = false
) : IRequest<PagedResponseDto<ProjectBaseDto>>;