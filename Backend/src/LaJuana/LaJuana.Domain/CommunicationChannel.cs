using LaJuana.Domain.Common;

namespace LaJuana.Domain
{
    public abstract class CommunicationChannel: BaseDomainModel
    {
        public Guid PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
