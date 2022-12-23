using LaJuana.Application.Features.Programs.Commands.CreatePrograms;
using LaJuana.Application.Features.Programs.Commands.DeletePrograms;
using LaJuana.Application.Features.Programs.Commands.UpdatePrograms;
using LaJuana.Application.Features.Programs.Queries.FindProgramsById;
using LaJuana.Application.Features.Programs.Queries.FindPromansFileById;
using LaJuana.Application.Features.Programs.Queries.GetListPrograms;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Drawing;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
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

        [HttpGet("FindProgramsFileById/{id}")]
        //[Authorize]
        [ProducesResponseType(typeof(ProgramsFullVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> FindProgramsFileById(Guid id)
        {
            var query = new FindProgramsFileByIdQuery(id);
            var fileBase64 = await _mediator.Send(query);
            return File(Convert.FromBase64String(fileBase64), "image/png");
        }
       
    }
}
