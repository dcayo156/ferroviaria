using LaJuana.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Domain
{
    public class RelationshipDetail: BaseDomainModel
    {
        public Guid RelationshipID { get; set; }
        public Guid PersonID { get; set; }
        public Guid RelationshipTypeID { get; set; }

        public bool IsNeutral { get; set; }
        public virtual Person? Person { get; set; }
        public virtual RelationshipType? RelationshipType { get; set; }
        public virtual Relationship? Relationship { get; set; }
    }
}
