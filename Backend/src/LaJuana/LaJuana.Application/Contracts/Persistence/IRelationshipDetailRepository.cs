using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IRelationshipDetailRepository : IAsyncRepository<RelationshipDetail>
    {
        Task<IEnumerable<PersonByRelationshipTypeVm>> GetPersonByRelationshipType(Guid relationshipTypeId, Gender gender);
        Task<IEnumerable<RelationShipByPersonVm>> GetRelationShipsByPersonId(Guid PersonId);


    }
}
