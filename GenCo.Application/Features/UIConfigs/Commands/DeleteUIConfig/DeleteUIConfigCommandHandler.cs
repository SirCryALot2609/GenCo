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

namespace GenCo.Application.Features.UIConfigs.Commands.DeleteUIConfig
{
    public class DeleteUIConfigCommandHandler(IUIConfigRepository repository, IMapper mapper)
        : IRequestHandler<DeleteUIConfigCommand, BaseDeleteResponseDto>
    {
        private readonly IUIConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseDeleteResponseDto> Handle(DeleteUIConfigCommand request, CancellationToken cancellationToken)
        {
            var uIConfig = await _repository.GetByIdAsync(request.Request.Id);
            if (uIConfig == null)
            {
                return new BaseDeleteResponseDto
                {
                    Success = false,
                    Message = "UI config  not found.",
                    DeleteAt = DateTime.UtcNow,
                    DeleteBy = "system",
                };
            }
            await _repository.DeleteAsync(uIConfig);
            var deleted = await _repository.GetByIdAsync(uIConfig.Id);
            return new BaseDeleteResponseDto
            {
                Success = deleted.IsDelete,
                Message = deleted.IsDelete
                    ? "UI config deleted successfully."
                    : "Failed to delete UI config.",
                DeleteAt = deleted.DeleteAt,
                DeleteBy = deleted.DeleteBy,
            };
        }
    }
}
