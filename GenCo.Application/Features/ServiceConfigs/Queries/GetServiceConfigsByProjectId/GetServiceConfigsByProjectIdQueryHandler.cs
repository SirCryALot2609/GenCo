using AutoMapper;
using GenCo.Application.DTOs.ServiceConfig.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Queries.GetServiceConfigsByProjectId
{
    public class GetServiceConfigsByProjectIdQueryHandler(IServiceConfigRepository repository, IMapper mapper)
        : IRequestHandler<GetServiceConfigsByProjectIdQuery, ServiceConfigsByProjectIdResponseDto>
    {
        private readonly IServiceConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceConfigsByProjectIdResponseDto> Handle(GetServiceConfigsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var serviceConfigs = await _repository.GetServiceConfigsByProjectIdAsync(request.ProjecId);
            if (serviceConfigs == null || serviceConfigs.Count == 0)
            {
                return new ServiceConfigsByProjectIdResponseDto
                {
                    Success = false,
                    Message = "No service configs found for the given entity.",
                    RetrievedAt = DateTime.UtcNow
                };
            }
            var serviceConfigDtos = _mapper.Map<List<ServiceConfigResponseDto>>(serviceConfigs);
            return new ServiceConfigsByProjectIdResponseDto
            {
                Success = true,
                Message = "Service configs retrieved successfully.",
                RetrievedAt = DateTime.UtcNow,
                ServiceConfig = serviceConfigDtos
            };
        }
    }
}
