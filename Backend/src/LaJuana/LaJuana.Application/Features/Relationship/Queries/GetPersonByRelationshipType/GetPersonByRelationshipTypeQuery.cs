using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;

namespace LaJuana.Application.Features.Relationship.Queries.GetPersonByRelationshipType
{
    public class GetPersonByRelationshipTypeQuery : IRequest<List<PersonByRelationshipTypeVm>>
    {
        public Guid RelationshipTypeId { get; set; } 
        public Gender Gender { get; set; } = 0;
        public Boolean FromLucene { get; set; } = false;

        public GetPersonByRelationshipTypeQuery(Guid relationshipTypeId, Gender gender,Boolean fromLucene )
        {
            RelationshipTypeId = relationshipTypeId;
            Gender = gender;
            FromLucene = fromLucene;
        }
    }
}
