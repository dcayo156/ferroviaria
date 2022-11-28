using LaJuana.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Domain
{
    public class Relationship: BaseDomainModel
    {
        public Relationship()
        {
            RelationshipDetails = new HashSet<RelationshipDetail>();
        }
        //public bool IsNeutral { get; set; }
        public virtual ICollection<RelationshipDetail> RelationshipDetails { get; set; }
    }
}
