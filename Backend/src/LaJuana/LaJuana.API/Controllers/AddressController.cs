using LaJuana.Application.Features.Addresses.Commands.CreateAddress;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using LaJuana.Application.Features.Addresses.Commands.UpdateAddress;
using LaJuana.Application.Features.Addresses.Commands.DeleteAddress;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
    public class AddressController : ControllerBase
    {
        private IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateAddress")]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateTag([FromBody] CreateAddressCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut("UpdateAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateTag([FromBody] UpdateAddressCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeleteAddress/{id}", Name = "DeleteAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteAddress(Guid id)
        {
            var command = new DeleteAddressCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }


    }
}
