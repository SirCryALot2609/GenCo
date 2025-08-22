using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
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
        : IRequestHandler<UpdateEntityCommand, UpdateEntityResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<UpdateEntityResponseDto> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken)
                ?? throw new KeyNotFoundException("Entity not found");
            _mapper.Map(request.Request, entity);
            await _repository.UpdateAsync(entity, cancellationToken);
            return _mapper.Map<UpdateEntityResponseDto>(entity);
        }
    }
}
