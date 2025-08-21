using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<BaseCreateResponseDto>
    {
        public CreateProjectRequestDto Request { get; set; } = default!;
    }
}
