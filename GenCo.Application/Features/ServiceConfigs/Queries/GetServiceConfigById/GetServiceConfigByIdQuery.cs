using GenCo.Application.DTOs.ServiceConfig.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Queries.GetServiceConfigById
{
    public class GetServiceConfigByIdQuery(Guid Id) : IRequest<ServiceConfigResponseDto>
    {
        public Guid Id { get; set; } = Id;
    }
}
