using LaJuana.Application.Features.Relationship.Commands.CreateRelationship;
using LaJuana.Application.Features.Relationship.Commands.DeleteRelationship;
using LaJuana.Application.Features.Relationship.Queries.GetRelationShipsByPersonId;
using LaJuana.Application.Features.Relationship.Queries.GetPersonByRelationshipType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using Microsoft.AspNetCore.Authorization;
using LaJuana.Application.Features.Relationship.Queries.GetRelationShipTypes;
using LaJuana.Application.Features.RelationshipType.Commands.UpdateRelationshipType;
using LaJuana.Application.Features.RelationshipType.Commands.CreateRelationshipType;
using LaJuana.Application.Features.Relationship.Queries.GetRelationShipGroupTypes;
using LaJuana.Application.Features.Relationship.Commands.DeleteRelationshipType;

namespace LaJuana.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class RelationshipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelationshipController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost(Name = "CreateRelationship")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateRelationship([FromBody] CreateRelationshipCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("CreateRelationshipType")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateRelationshipType([FromBody] CreateRelationshipTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdateRelationshipType")]
        [Authorize]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateRelationshipType([FromBody] UpdateRelationshipTypeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteRelationshipType/{id}", Name = "DeleteRelationshipType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteRelationshipType(Guid id)
        {
            var command = new DeleteRelationshipTypeCommand
            {
                Id = id
            };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteRelationship")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteRelationship(Guid id)
        {
            var command = new DeleteRelationshipCommand
            {
                Id = id
            };
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpGet("GetRelationShipsByPersonId/{personId}")]
        [Authorize]
        [ProducesResponseType(typeof(RelationShipByPersonVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RelationShipByPersonVm>>> GetRelationShipsByPersonId(Guid personId)
        {
            var query = new GetRelationShipsByPersonIdQuery(personId);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }

        [HttpGet("GetPersonByRelationshipType/", Name = "GetPersonByRelationshipType")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PersonByRelationshipTypeVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PersonByRelationshipTypeVm>>> GetPersonByRelationshipType(Guid relationTypeId, Gender gender,Boolean fromLucene)
        {
            var query = new GetPersonByRelationshipTypeQuery(relationTypeId, gender,fromLucene);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }

        [HttpGet("GetRelationShipTypes/", Name = "GetRelationShipTypes")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<RelationshipTypesVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RelationshipTypesVM>>> GetRelationShipTypes()
        {
            var query = new GetRelationShipTypesQuery();
            var relationshipTypes = await _mediator.Send(query);
            return Ok(relationshipTypes);
        }

        [HttpGet("GetRelationShipGroupedTypes/", Name = "GetRelationShipGroupedTypes")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<RelationshipGroupTypesVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RelationshipGroupTypesVM>>> GetRelationShipGroupedTypes()
        {
            var query = new GetRelationShipGroupTypesQuery();
            var relationshipTypes = await _mediator.Send(query);
            return Ok(relationshipTypes);
        }
    }
}
