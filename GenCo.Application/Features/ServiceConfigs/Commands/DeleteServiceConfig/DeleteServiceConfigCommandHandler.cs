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
        : IRequestHandler<DeleteServiceConfigCommand, BaseDeleteResponseDto>
    {
        private readonly IServiceConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseDeleteResponseDto> Handle(DeleteServiceConfigCommand request, CancellationToken cancellationToken)
        {
            var serviceConfig = await _repository.GetByIdAsync(request.Request.Id);
            if (serviceConfig == null)
            {
                return new BaseDeleteResponseDto
                {
                    Success = false,
                    Message = "Service config not found.",
                    DeleteAt = DateTime.UtcNow,
                    DeleteBy = "system",
                };
            }
            await _repository.DeleteAsync(serviceConfig);
            var deleted = await _repository.GetByIdAsync(serviceConfig.Id);
            return new BaseDeleteResponseDto
            {
                Success = deleted.IsDelete,
                Message = deleted.IsDelete
                    ? "Service config deleted successfully."
                    : "Failed to delete service config.",
                DeleteAt = deleted.DeleteAt,
                DeleteBy = deleted.DeleteBy,
            };
        }
    }
}
