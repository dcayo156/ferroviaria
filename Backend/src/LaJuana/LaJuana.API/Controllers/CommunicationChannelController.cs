using LaJuana.Application.Features.Mails.Commands.CreateMail;
using LaJuana.Application.Features.Mails.Commands.UpdateMail;
using LaJuana.Application.Features.Mails.Commands.DeleteMail;
using LaJuana.Application.Features.Phones.Commands.CreatePhone;
using LaJuana.Application.Features.Phones.Commands.UpdatePhone;
using LaJuana.Application.Features.Phones.Commands.DeletePhone;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace LaJuana.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CommunicationChannelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunicationChannelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region "MAIL"
        [HttpPost("CreateMail")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateMail([FromBody] CreateMailCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdateMail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateMail([FromBody] UpdateMailCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeleteMail/{id}", Name = "DeleteMail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteMail(Guid id)
        {
            var command = new DeleteMailCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
        #endregion

        #region "PHONE"
        [HttpPost("CreatePhone")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreatePhone([FromBody] CreatePhoneCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdatePhone")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePhone([FromBody] UpdatePhoneCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeletePhone/{id}", Name = "DeletePhone")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeletePhone(Guid id)
        {
            var command = new DeletePhoneCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
        #endregion
    }
}
