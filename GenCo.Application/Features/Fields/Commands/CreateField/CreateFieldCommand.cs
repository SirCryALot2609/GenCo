using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.CreateField
{
    public class CreateFieldCommand : IRequest<BaseCreateResponseDto>
    {
        public CreateFieldRequestDto Request { get; set; } = default!;
    }
}
