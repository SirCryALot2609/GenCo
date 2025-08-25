using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator.Requests;
using GenCo.Application.DTOs.FieldValidator.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.FieldValidators.Commands.DeleteFieldValidator
{
    public record DeleteFieldValidatorCommand(DeleteFieldValidatorRequestDto Request)
        : IRequest<BaseResponseDto<BoolResultDto>>
    {
    }
}
