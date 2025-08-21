using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.DeleteEntity
{
    public class DeleteEntityCommand : IRequest<BaseDeleteResponseDto>
    {
        public DeleteEntityRequestDto Request { get; set; } = default!;
    }
}
