using GenCo.Application.DTOs.UIConfig.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.UIConfigs.Queries.GetUIConfigById
{
    public class GetUIConfigByIdQuery(Guid Id) : IRequest <UIConfigResponseDto>
    {
        public Guid Id { get; set; } = Id;
    }
}
