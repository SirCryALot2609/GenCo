using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Commands.CreateServiceConfig
{
    public class CreateServiceConfigCommandHandler(IServiceConfigRepository repository, IMapper mapper)
        : IRequestHandler<CreateServiceConfigCommand, BaseCreateResponseDto>
    {
        private readonly IServiceConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseCreateResponseDto> Handle(CreateServiceConfigCommand request, CancellationToken cancellationToken)
        {
            var serviceConfig = _mapper.Map<ServiceConfig>(request.Request);
            serviceConfig.CreatedAt = DateTime.UtcNow;
            serviceConfig.CreatedBy = "system";
            var created = await _repository.AddAsync(serviceConfig);
            return new BaseCreateResponseDto
            {
                Success = true,
                Message = "Service config created successfully.",
                CreatedAt = created.CreatedAt,
                CreatedBy = created.CreatedBy,
            };
        }
    }
}
