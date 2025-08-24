using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity;
using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.RestoreEntity
{
    public record RestoreEntityCommand(Guid Id)
        :IRequest<BaseResponseDto<EntityResponseDto>>
    {
    }
}
