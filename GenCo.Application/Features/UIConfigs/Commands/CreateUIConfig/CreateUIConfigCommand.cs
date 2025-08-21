using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.UIConfig.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.UIConfigs.Commands.CreateUIConfig
{
    public class CreateUIConfigCommand : IRequest<BaseCreateResponseDto>
    {
        public CreateUIConfigRequestDto Request { get; set; } = default!;
    }
}
