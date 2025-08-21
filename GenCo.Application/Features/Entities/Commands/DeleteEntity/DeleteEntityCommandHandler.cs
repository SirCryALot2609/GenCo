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
                : IRequestHandler<DeleteEntityCommand, BaseDeleteResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseDeleteResponseDto> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Request.Id);
            if (entity == null)
            {
                return new BaseDeleteResponseDto
                {
                    Success = false,
                    Message = "Entity not found.",
                    DeleteAt = DateTime.UtcNow,
                    DeleteBy = "system"
                };
            }
            await _repository.DeleteAsync(entity);
            var deleted = await _repository.GetByIdAsync(entity.Id);
            return new BaseDeleteResponseDto
            {
                Success = deleted.IsDelete,
                Message = deleted.IsDelete
                    ? "Entity deleted successfully."
                    : "Failed to delete entity.",
                DeleteAt = deleted.DeleteAt,
                DeleteBy = deleted.DeleteBy,
            };
        }
    }
}
