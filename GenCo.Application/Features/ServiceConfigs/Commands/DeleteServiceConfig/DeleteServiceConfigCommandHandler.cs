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

namespace GenCo.Application.Features.ServiceConfigs.Commands.DeleteServiceConfig
{
    public class DeleteServiceConfigCommandHandler(IServiceConfigRepository repository, IMapper mapper)
        : IRequestHandler<DeleteServiceConfigCommand, BaseUpdateResponseDto>
    {
        private readonly IServiceConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(DeleteServiceConfigCommand request, CancellationToken cancellationToken)
        {
            var serviceConfig = await _repository.GetByIdAsync(request.Request.Id);
            if (serviceConfig == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "Service config not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system",
                };
            }
            var deleted = await _repository.DeleteAsync(serviceConfig);
            return new BaseUpdateResponseDto
            {
                Success = deleted,
                Message = deleted
                    ? "Service config deleted successfully."
                    : "Failed to delete service config.",
                UpdatedAt = serviceConfig.CreatedAt,
                UpdatedBy = serviceConfig.UpdateBy,
            };
        }
    }
}
