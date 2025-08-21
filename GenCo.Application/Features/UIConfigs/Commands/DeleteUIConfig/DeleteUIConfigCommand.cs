using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.UIConfig.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.UIConfigs.Commands.DeleteUIConfig
{
    public class DeleteUIConfigCommand : IRequest<BaseDeleteResponseDto>
    {
        public DeleteUIConfigRequestDto Request { get; set; } = default!;
    }
}
