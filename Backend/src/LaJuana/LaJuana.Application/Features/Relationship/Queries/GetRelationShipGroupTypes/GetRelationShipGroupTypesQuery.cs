using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Relationship.Queries.GetRelationShipGroupTypes
{
    public class GetRelationShipGroupTypesQuery : IRequest<List<RelationshipGroupTypesVM>>
    {
        public GetRelationShipGroupTypesQuery()
        {
        }
    }
}
