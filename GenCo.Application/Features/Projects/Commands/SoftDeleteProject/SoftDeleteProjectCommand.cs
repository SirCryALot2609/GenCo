using GenCo.Application.DTOs.Project.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.SoftDeleteProject
{
    public record SoftDeleteProjectCommand(Guid Id) : IRequest<DeleteProjectResponseDto>;
}
