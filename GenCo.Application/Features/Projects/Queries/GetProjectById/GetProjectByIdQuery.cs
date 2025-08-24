using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Queries.GetProjectById
{
    public record GetProjectByIdQuery(Guid ProjectId, bool IncludeAllCollections = false)
    : IRequest<BaseResponseDto<ProjectDetailDto>>;
}
