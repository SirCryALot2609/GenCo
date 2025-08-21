using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.UIConfigs.Commands.UpdateUIConfig
{
    public class UpdateUIConfigCommandHandler(IUIConfigRepository repository, IMapper mapper)
        : IRequestHandler<UpdateUIConfigCommand, BaseUpdateResponseDto>
    {
        private readonly IUIConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseUpdateResponseDto> Handle(UpdateUIConfigCommand request, CancellationToken cancellationToken)
        {
            var uIConfig = await _repository.GetByIdAsync(request.Request.Id);
            if (uIConfig == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "UI config not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
            }
            _mapper.Map(request.Request, uIConfig);
            var updated = await _repository.UpdateAsync(uIConfig);
            return new BaseUpdateResponseDto
            {
                Success = true,
                Message = "UI config updated successfully.",
                UpdatedAt = updated.UpdateAt,
                UpdatedBy = updated.UpdateBy,
            };
        }
    }
}
