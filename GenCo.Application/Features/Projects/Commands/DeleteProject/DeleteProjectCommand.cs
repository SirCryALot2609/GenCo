using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<BaseUpdateResponseDto>
    {
        public DeleteProjectRequestDto Request { get; set; } = default!;
    }
}
