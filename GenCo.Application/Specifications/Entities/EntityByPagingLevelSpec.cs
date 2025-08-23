using GenCo.Application.Specifications.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Entities
{
    public class EntityByPagingLevelSpec : BaseSpecification<Entity>
    {
        /// <summary>
        /// Paging level: 
        /// 1 = Entity + Fields + Validators
        /// 2 = Level 1 + Relations + ToEntity + ToEntity.Fields + Validators
        /// 3 = Level 2 + ToEntity.Relations + TargetEntities recursively
        /// </summary>
        /// <param name="rootEntityId">Id của Entity gốc</param>
        /// <param name="pagingLevel">Độ sâu load</param>
        /// <param name="skip">Paging skip</param>
        /// <param name="take">Paging take</param>
        public EntityByPagingLevelSpec(Guid rootEntityId, int pagingLevel = 1, int skip = 0, int take = 10)
            : base(e => e.Id == rootEntityId)
        {
            // Level 1: Entity + Fields + Validators
            AddInclude(e => e.Fields);
            AddInclude(e => e.Fields.Select(f => f.Validators));

            AddInclude(e => e.Project);

            if (pagingLevel >= 2)
            {
                // Include immediate Relations
                AddInclude(e => e.FromRelations);
                AddInclude(e => e.FromRelations.Select(r => r.ToEntity));
                AddInclude(e => e.FromRelations.Select(r => r.ToEntity.Fields));
                AddInclude(e => e.FromRelations.SelectMany(r => r.ToEntity.Fields).Select(f => f.Validators));

                AddInclude(e => e.ToRelations);
                AddInclude(e => e.ToRelations.Select(r => r.FromEntity));
                AddInclude(e => e.ToRelations.Select(r => r.FromEntity.Fields));
                AddInclude(e => e.ToRelations.SelectMany(r => r.FromEntity.Fields).Select(f => f.Validators));
            }

            if (pagingLevel >= 3)
            {
                AddRelationsRecursive(currentLevel: 3, maxLevel: pagingLevel);
            }

            ApplyOrderBy(e => e.Name);
            ApplyPaging(skip, take);
        }

        private void AddRelationsRecursive(int currentLevel, int maxLevel)
        {
            if (currentLevel > maxLevel) return;

            // FromRelations recursive
            AddInclude(e => e.FromRelations.Select(r => r.ToEntity.FromRelations));
            AddInclude(e => e.FromRelations.Select(r => r.ToEntity.FromRelations.Select(rr => rr.ToEntity)));
            AddInclude(e => e.FromRelations.SelectMany(r => r.ToEntity.FromRelations)
                                           .SelectMany(rr => rr.ToEntity.Fields)
                                           .Select(f => f.Validators));

            // ToRelations recursive
            AddInclude(e => e.ToRelations.Select(r => r.FromEntity.ToRelations));
            AddInclude(e => e.ToRelations.Select(r => r.FromEntity.ToRelations.Select(rr => rr.FromEntity)));
            AddInclude(e => e.ToRelations.SelectMany(r => r.FromEntity.ToRelations)
                                         .SelectMany(rr => rr.FromEntity.Fields)
                                         .Select(f => f.Validators));

            // Recursive call
            AddRelationsRecursive(currentLevel + 1, maxLevel);
        }
    }
}
