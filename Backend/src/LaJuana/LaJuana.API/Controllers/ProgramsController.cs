using LaJuana.Application.Features.Programs.Commands.CreatePrograms;
using LaJuana.Application.Features.Programs.Commands.DeletePrograms;
using LaJuana.Application.Features.Programs.Commands.UpdatePrograms;
using LaJuana.Application.Features.Programs.Queries.FindProgramsById;
using LaJuana.Application.Features.Programs.Queries.GetListPrograms;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private IMediator _mediator;

        public ProgramsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreatePrograms")]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreatePrograms([FromBody] CreateProgramsCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut("UpdatePrograms")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePrograms([FromBody] UpdateProgramsCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeletePrograms/{id}", Name = "DeletePrograms")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeletePrograms(Guid id)
        {
            var command = new DeleteProgramsCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
        [HttpGet("GetPrograms")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<ProgramsFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProgramsFullVm>>> GetProgramss()
        {
            var query = new GetListProgramsQuery();
            var Programss = await _mediator.Send(query);
            return Ok(Programss);
        }
        [HttpGet("FindProgramsById/{id}")]
        //[Authorize]
        [ProducesResponseType(typeof(ProgramsFullVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProgramsFullVm>>> FindProgramsById(Guid id)
        {
            var query = new FindProgramsByIdQuery(id);
            var Programs = await _mediator.Send(query);
            return Ok(Programs);
        }
    }
}
