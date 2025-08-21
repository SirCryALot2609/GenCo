using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.ServiceConfig.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Commands.CreateServiceConfig
{
    public class CreateServiceConfigCommand : IRequest<BaseCreateResponseDto>
    {
        public CreateServiceConfigRequestDto Request { get; set; } = default!;
    }
}
