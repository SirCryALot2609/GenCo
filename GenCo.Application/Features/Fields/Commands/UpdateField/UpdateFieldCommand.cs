using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.UpdateField
{
    public class UpdateFieldCommand : IRequest<BaseUpdateResponseDto>
    {
        public UpdateFieldRequestDto Request { get; set; } = default!;
    }
}
