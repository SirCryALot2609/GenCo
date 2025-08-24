using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Queries.GetFieldById
{
    public record GetFieldByIdQuery(Guid Id, bool IncludeDetails = false) 
        : IRequest<BaseResponseDto<FieldResponseDto>>
    {
    }
}
