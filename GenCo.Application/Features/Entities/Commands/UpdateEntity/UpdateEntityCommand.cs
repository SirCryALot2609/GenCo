using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.UpdateEntity
{
    public class UpdateEntityCommand : IRequest<BaseUpdateResponseDto>
    {
        public UpdateEntityRequestDto Request { get; set; } = default!;
    }
}
