using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Relations
{
    public class RelationByIdSpec(Guid fieldId) : BaseSpecification<Relation>(f => f.Id == fieldId)
    {
    }
}
