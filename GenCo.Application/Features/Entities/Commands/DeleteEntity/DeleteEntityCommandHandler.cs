using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
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
                : IRequestHandler<DeleteEntityCommand, DeleteEntityResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<DeleteEntityResponseDto> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken)
                ?? throw new KeyNotFoundException("Entity not found");
            await _repository.SoftDeleteAsync(entity, cancellationToken);
            return new DeleteEntityResponseDto
            {
                Id = request.Request.Id,
                Deleted = true
            };
        }
    }
}
