using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Relationship.Commands.CreateRelationship
{
    public class CreateRelationshipCommand : IRequest<Guid>
    {
       public Guid PersonId { get; set; }
        public RelationshipCommand? Relation { get; set; }
    }
}
