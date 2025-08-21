using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.UpdateEntity
{
    public class UpdateEntityCommandHandler(IProjectRepository repository, IMapper mapper)
        : IRequestHandler<UpdateEntityCommand, BaseUpdateResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseUpdateResponseDto> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Request.Id);
            if (entity == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "Entity not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
            }
            _mapper.Map(request.Request, entity);
            await _repository.UpdateAsync(entity);
            return new BaseUpdateResponseDto
            {
                Success = true,
                Message = "Entity updated successfully.",
                UpdatedAt = entity.UpdateAt,
                UpdatedBy = entity.UpdateBy,
            };
        }
    }
}
