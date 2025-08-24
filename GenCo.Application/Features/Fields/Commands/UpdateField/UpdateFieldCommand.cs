using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Requests;
using GenCo.Application.DTOs.Field.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.UpdateField
{
    public record UpdateFieldCommand(UpdateFieldRequestDto Request) 
        : IRequest<BaseResponseDto<FieldResponseDto>>
    {
    }
}
