using GenCo.Application.DTOs.UIConfig.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.UIConfigs.Queries.GetUIConfigsByProjectId
{
    public class GetUIConfigsByProjectIdQuery(Guid projectId) : IRequest<UIConfigsByProjectIdResponseDto>
    {
        public Guid ProjectId { get; set; } = projectId;
    }
}
