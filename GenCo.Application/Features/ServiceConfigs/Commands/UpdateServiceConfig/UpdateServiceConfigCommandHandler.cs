using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.ServiceConfigs.Commands.UpdateServiceConfig
{
    public class UpdateServiceConfigCommandHandler(IServiceConfigRepository repository, IMapper mapper)
        : IRequestHandler<UpdateServiceConfigCommand, BaseUpdateResponseDto>
    {
        private readonly IServiceConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(UpdateServiceConfigCommand request, CancellationToken cancellationToken)
        {
            var serviceConfig = await _repository.GetByIdAsync(request.Request.Id);
            if (serviceConfig == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "Relation not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
            }
            _mapper.Map(request.Request, serviceConfig);
            await _repository.UpdateAsync(serviceConfig);
            return new BaseUpdateResponseDto
            {
                Success = true,
                Message = "Relation updated successfully.",
                UpdatedAt = serviceConfig.UpdateAt,
                UpdatedBy = serviceConfig.UpdateBy,
            };
        }
    }
}
