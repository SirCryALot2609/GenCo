using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Requests;
using GenCo.Application.DTOs.Project.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.UpdateProject
{
    public record UpdateProjectCommand(UpdateProjectRequestDto Request)
    : IRequest<UpdateProjectResponseDto>;
}
