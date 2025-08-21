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

namespace GenCo.Application.Features.UIConfigs.Queries.GetUIConfigsByProjectId
{
    public class GetUIConfigsByProjectIdQueryHandler(IUIConfigRepository repository, IMapper mapper)
        : IRequestHandler<GetUIConfigsByProjectIdQuery, UIConfigsByProjectIdResponseDto>
    {
        private readonly IUIConfigRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<UIConfigsByProjectIdResponseDto> Handle(GetUIConfigsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var uIConfigs = await _repository.GetUIConfigsByProjectIdAsync(request.ProjectId);
            if (uIConfigs == null || uIConfigs.Count == 0)
            {
                return new UIConfigsByProjectIdResponseDto
                {
                    Success = false,
                    Message = "No UI configs found for the given project.",
                    RetrievedAt = DateTime.UtcNow
                };
            }
            var uIConfigDtos = _mapper.Map<List<UIConfigResponseDto>>(uIConfigs);
            return new UIConfigsByProjectIdResponseDto
            {
                Success = true,
                Message = "UI configs retrieved successfully.",
                RetrievedAt = DateTime.UtcNow,
                UIConfigs = uIConfigDtos
            };
        }
    }
}
