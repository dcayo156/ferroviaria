using LaJuana.Application.Features.Organizations.Queries.FindOrganizationByTagId;
using LaJuana.Application.Features.Organizations.Queries.GetOrganizationsList;
using LaJuana.Application.Features.Organizations.Queries.GetOrganizationAll;
using LaJuana.Application.Features.Organizations.Commands.CreateOrganization;
using LaJuana.Application.Features.Organizations.Commands.UpdateOrganization;
using LaJuana.Application.Features.Organizations.Commands.DeleteOrganization;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("FindOrganizationByTagId/{TagId}", Name = "FindOrganizationByTagId")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<OrganizationVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrganizationVm>>> FindOrganizationByTagIdQuery(Guid TagId)
        {
            var query = new FindOrganizationByTagIdQuery(TagId);
            var organizations = await _mediator.Send(query);
            return Ok(organizations);
        }

        [HttpGet("GetOrganization/{username}", Name = "GetOrganization")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<OrganizationVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrganizationVm>>> GetOrganizationsByUsername(string username)
        {
            var query = new GetOrganizationsListQuery(username);
            var organizations = await _mediator.Send(query);
            return Ok(organizations);
        }

        [HttpGet("GetOrganizationAll")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<OrganizationVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrganizationVm>>> GetOrganizationAll()
        {
            var query = new GetOrganizationAllQuery();
            var organizations = await _mediator.Send(query);
            return Ok(organizations);
        }

        [HttpPost("CreateOrganization")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateOrganization([FromBody] CreateOrganizationCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdateOrganization")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrganization([FromBody] UpdateOrganizationCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeleteOrganization/{id}", Name = "DeleteOrganization")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrganization(Guid id)
        {
            var command = new DeleteOrganizationCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }

}
