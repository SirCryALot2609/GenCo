using AutoMapper;
using GenCo.Application.BusinessRules.RelationJoinTables;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.UpdateRelationJoinTable;


public class UpdateRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IRelationJoinTableBusinessRules businessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateRelationJoinTableCommand, BaseResponseDto<RelationJoinTableResponseDto>>
{
    public async Task<BaseResponseDto<RelationJoinTableResponseDto>> Handle(UpdateRelationJoinTableCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        // 🧩 1. Kiểm tra entity tồn tại
        var entity = await repository.GetByIdAsync(dto.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<RelationJoinTableResponseDto>.Fail("RelationJoinTable not found");

        // 🧩 2. Kiểm tra Relation có tồn tại không
        await businessRules.EnsureRelationExistsAsync(dto.RelationId, cancellationToken);

        // 🧩 3. Kiểm tra loại quan hệ có phải Many-to-Many không
        await businessRules.EnsureRelationTypeIsManyToManyAsync(dto.RelationId, cancellationToken);

        // 🧩 4. Đảm bảo tên JoinTable không bị trùng trong cùng Relation
        await businessRules.EnsureJoinTableNameUniqueAsync(dto.RelationId, dto.JoinTableName, cancellationToken);

        // 🧩 5. Ánh xạ dữ liệu và kiểm tra key
        mapper.Map(dto, entity);
        businessRules.EnsureValidKeys(entity);

        // 🧩 6. Cập nhật metadata
        entity.UpdatedAt = DateTime.UtcNow;

        // 🧩 7. Lưu thay đổi
        await repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var responseDto = mapper.Map<RelationJoinTableResponseDto>(entity);
        return BaseResponseDto<RelationJoinTableResponseDto>.Ok(responseDto, "RelationJoinTable updated successfully");
    }
}
