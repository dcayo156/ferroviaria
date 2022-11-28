using LaJuana.Domain;
using LaJuana.Application.Features.Relationship.Commands.CreateRelationship;
using LaJuana.Application.Models.ViewModels;
namespace LaJuana.Application.Contracts.Persistence
{
    public interface IRelationshipRepository : IAsyncRepository<Relationship>
    {
        void AddEntityLucene(RelationshipCommand parent1,RelationshipCommand parent2);
        
        Task<IEnumerable<PersonByRelationshipTypeVm>> GetPersonByRelationshipTypeFromLucene(String gender,Guid typeId);
    }
}
