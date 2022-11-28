using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Relationship.Queries.GetRelationShipTypes
{
    public class GetRelationShipTypesQuery : IRequest<List<RelationshipTypesVM>>
    {
        public GetRelationShipTypesQuery()
        {
        }
    }
}
