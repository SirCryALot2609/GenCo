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

namespace GenCo.Application.Features.Entities.Commands.DeleteEntity
{
    public class DeleteEntityCommandHandler(IEntityRepository repository, IMapper mapper)
                : IRequestHandler<DeleteEntityCommand, BaseUpdateResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
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
            var deleted = await _repository.DeleteAsync(entity);
            return new BaseUpdateResponseDto
            {
                Success = deleted,
                Message = deleted
                    ? "Entity deleted successfully."
                    : "Failed to delete entity.",
                UpdatedAt = entity.UpdateAt,
                UpdatedBy = entity.UpdateBy,
            };
        }
    }
}
