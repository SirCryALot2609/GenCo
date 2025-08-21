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

namespace GenCo.Application.Features.UIConfigs.Commands.CreateUIConfig
{
    public class CreateUIConfigCommandHandler(IUIConfigRepository repository, IMapper mapper)
        : IRequestHandler<CreateUIConfigCommand, BaseCreateResponseDto>
    {
        private readonly IUIConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseCreateResponseDto> Handle(CreateUIConfigCommand request, CancellationToken cancellationToken)
        {
            var uIConfig = _mapper.Map<UIConfig>(request);
            uIConfig.CreatedAt = DateTime.Now;
            uIConfig.CreatedBy = "system";
            var created = await _repository.AddAsync(uIConfig);
            return new BaseCreateResponseDto
            {
                Success = true,
                Message = "UI config created successfully.",
                CreatedAt = created.CreatedAt,
                CreatedBy = created.CreatedBy,
            };
        }
    }
}
