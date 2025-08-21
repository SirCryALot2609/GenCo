using GenCo.Application.DTOs.Project.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<ProjectSummaryResponseDto>
    {
        public GetAllProjectsQuery() { }
    }
}
