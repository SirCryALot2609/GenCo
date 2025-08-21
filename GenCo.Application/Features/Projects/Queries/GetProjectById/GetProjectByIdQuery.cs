using GenCo.Application.DTOs.Project.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery(Guid Id) : IRequest<ProjectResponseDto>
    {
        public Guid Id { get; set; } = Id;
    }
}
