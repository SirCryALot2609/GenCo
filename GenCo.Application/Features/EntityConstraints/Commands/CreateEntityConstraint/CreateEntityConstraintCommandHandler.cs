using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraint.Response;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Commands.CreateEntityConstraint;


public class CreateEntityConstraintCommandHandler(
    IGenericRepository<EntityConstraint> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateEntityConstraintCommand, BaseResponseDto<EntityConstraintResponseDto>>
{
    public async Task<BaseResponseDto<EntityConstraintResponseDto>> Handle(CreateEntityConstraintCommand request, CancellationToken cancellationToken)
    {
        var constraint = mapper.Map<EntityConstraint>(request.Request);
        constraint.Id = Guid.NewGuid();
        constraint.CreatedAt = DateTime.UtcNow;
        constraint.UpdatedAt = null;

        await repository.AddAsync(constraint, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<EntityConstraintResponseDto>(constraint);
        return BaseResponseDto<EntityConstraintResponseDto>.Ok(dto, "EntityConstraint created successfully");
    }
}