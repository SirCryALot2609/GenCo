using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Requests;
using GenCo.Application.DTOs.Entity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.CreateEntity
{
    public record CreateEntityCommand(CreateEntityRequestDto Request)
        : IRequest<CreateEntityResponseDto>;
}
