using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Relationship.Queries.GetRelationShipsByPersonId
{
	public class GetRelationShipsByPersonIdQuery : IRequest<List<RelationShipByPersonVm>>
	{
        public Guid PersonId { get; set; }

        public GetRelationShipsByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}

