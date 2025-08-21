using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.ServiceConfig.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Commands.UpdateServiceConfig
{
    public class UpdateServiceConfigCommand : IRequest<BaseUpdateResponseDto>
    {
        public UpdateServiceConfigRequestDto Request { get; set; } = default!;
    }
}
