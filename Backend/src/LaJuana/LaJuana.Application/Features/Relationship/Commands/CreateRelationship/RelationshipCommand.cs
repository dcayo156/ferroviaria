using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Relationship.Commands.CreateRelationship
{
    public class RelationshipCommand
    {
        public Guid PersonID { get; set; }
        public Guid RelationshipTypeID { get; set; }
        public bool IsNeutral { get; set; }
    }
}
