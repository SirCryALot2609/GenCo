using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project;
using GenCo.Application.DTOs.Project.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Queries.GetProjectList
{
    public record GetProjectListQuery(
    string? Keyword = null,
    int PageNumber = 1,
    int PageSize = 10,
    bool IncludeAllCollections = false) : IRequest<PagedResponseDto<ProjectListItemDto>>;
}
