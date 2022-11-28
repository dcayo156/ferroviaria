using LaJuana.Domain.Common;

namespace LaJuana.Domain
{
    public class PersonTag : BaseDomainModel
    {
        public Guid PersonId { get; set; }
        public Guid TagId { get; set; }
    }
}
