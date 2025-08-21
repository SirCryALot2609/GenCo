using AutoMapper;
using GenCo.Application.DTOs.Entity.Response;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.UIConfig.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.UIConfigs.Queries.GetUIConfigById
{
    public class GetUIConfigByIdQueryHandler(IUIConfigRepository repository, IMapper mapper)
        : IRequestHandler<GetUIConfigByIdQuery, UIConfigResponseDto>
    {
        private readonly IUIConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<UIConfigResponseDto> Handle(GetUIConfigByIdQuery request, CancellationToken cancellationToken)
        {
            var uIConfig = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<UIConfigResponseDto>(uIConfig);
        }
    }
}
