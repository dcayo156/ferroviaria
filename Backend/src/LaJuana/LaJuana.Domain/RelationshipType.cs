using LaJuana.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Domain
{
    public class RelationshipType : BaseDomainModel
    {
        public RelationshipType()
        {
            RelationshipDetails = new HashSet<RelationshipDetail>();
        }
        public Guid? RelationshipTypeRequiredID{ get; set; }
        public string FemaleDescription { get; set; } = string.Empty;
        public string MaleDescription { get; set; } = string.Empty;
        public string NeutralDescription { get; set; } = string.Empty;
        public virtual RelationshipType? RelationshipTypeRequired { get; set; }
        public virtual ICollection<RelationshipDetail> RelationshipDetails { get; set; }

    }
}
