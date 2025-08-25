using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.FieldValidator;
using GenCo.Application.DTOs.FieldValidator.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.FieldValidators.Commands.RestoreFieldValidator
{
    public record RestoreFieldValidatorCommand(Guid Id)
        :IRequest<BaseResponseDto<FieldValidatorResponseDto>>
    {
    }
}
