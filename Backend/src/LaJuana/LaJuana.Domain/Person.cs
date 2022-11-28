
using LaJuana.Domain.Common;

namespace LaJuana.Domain
{
    public abstract class Person : BaseDomainModel
    {
        public Person() {
            Addresses = new HashSet<Address>();
            Tags = new HashSet<Tag>();
            CommunicationChannels = new HashSet<CommunicationChannel>();
            RelationshipDetails = new HashSet<RelationshipDetail>();
        }
        
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<CommunicationChannel> CommunicationChannels { get; set; }
        public virtual ICollection<RelationshipDetail> RelationshipDetails { get; set; }
    }
}
