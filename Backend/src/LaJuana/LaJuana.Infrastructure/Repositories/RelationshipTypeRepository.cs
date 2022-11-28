using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Infrastructure.Repositories
{
    public class RelationshipTypeRepository : RepositoryBase<RelationshipType>, IRelationshipTypeRepository
    {
        public RelationshipTypeRepository(LaJuanaDbContext context) : base(context)
        {
        }
    }
}
