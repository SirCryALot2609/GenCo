using AutoMapper;
using GenCo.Application.BusinessRules.RelationJoinTables;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.CreateRelationJoinTable;

public class CreateRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IRelationJoinTableBusinessRules businessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateRelationJoinTableCommand, BaseResponseDto<RelationJoinTableResponseDto>>
{
    public async Task<BaseResponseDto<RelationJoinTableResponseDto>> Handle(
        CreateRelationJoinTableCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Request;

        // 🧩 1. Kiểm tra tồn tại Relation
        await businessRules.EnsureRelationExistsAsync(dto.RelationId, cancellationToken);

        // 🧩 2. Kiểm tra Relation có phải Many-to-Many không
        await businessRules.EnsureRelationTypeIsManyToManyAsync(dto.RelationId, cancellationToken);

        // 🧩 3. Kiểm tra JoinTableName không trùng trong cùng Relation
        await businessRules.EnsureJoinTableNameUniqueAsync(dto.RelationId, dto.JoinTableName, cancellationToken);

        // 🧩 4. Kiểm tra tính hợp lệ của LeftKey và RightKey (rỗng, ký tự đặc biệt, trùng nhau, v.v.)
        var tempEntity = mapper.Map<RelationJoinTable>(dto);
        businessRules.EnsureValidKeys(tempEntity);

        // 🧩 5. Tạo mới entity
        tempEntity.Id = Guid.NewGuid();
        tempEntity.CreatedAt = DateTime.UtcNow;
        tempEntity.UpdatedAt = null;

        await repository.AddAsync(tempEntity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<RelationJoinTableResponseDto>(tempEntity);
        return BaseResponseDto<RelationJoinTableResponseDto>.Ok(response, "RelationJoinTable created successfully");
    }
}
