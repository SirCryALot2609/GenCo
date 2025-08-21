using AutoMapper;
using GenCo.Application.DTOs.ServiceConfig.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Queries.GetServiceConfigById
{
    public class GetServiceConfigByIdQueryHandler(IServiceConfigRepository repository, IMapper mapper)
        : IRequestHandler<GetServiceConfigByIdQuery, ServiceConfigResponseDto>
    {
        private readonly IServiceConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceConfigResponseDto> Handle(GetServiceConfigByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceConfig = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<ServiceConfigResponseDto>(serviceConfig);
        }
    }
}
