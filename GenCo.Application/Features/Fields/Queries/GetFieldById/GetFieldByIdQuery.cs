using GenCo.Application.DTOs.Field.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Queries.GetFieldById
{
    public class GetFieldByIdQuery(Guid Id) : IRequest<FieldResponseDto>
    {
        public Guid Id { get; set; } = Id;
    }
}
