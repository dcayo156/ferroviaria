using LaJuana.Application.Features.Peoples.Commands;
using LaJuana.Application.Features.Peoples.Commands.DeletePeople;
using LaJuana.Application.Features.Peoples.Commands.IndexLucenePeople;
using LaJuana.Application.Features.Peoples.Commands.UpdatePeople;
using LaJuana.Application.Features.Peoples.Lucene.FindUserByNameLucene;
using LaJuana.Application.Features.Peoples.Queries.FindPeopleById;
using LaJuana.Application.Features.Peoples.Queries.FindPeopleByNationalId;
using LaJuana.Application.Features.Peoples.Queries.FindPeopleByTagId;
using LaJuana.Application.Features.Peoples.Queries.FindTagPeopleById;
using LaJuana.Application.Features.Peoples.Queries.GetListPeople;
using LaJuana.Application.Features.Peoples.Queries.GetPeoples;
using LaJuana.Application.Features.PersonTags.Commands;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{

    [ApiController]
    [Route("v1/[controller]")]
    public class PeopleController : ControllerBase
    {

        private IMediator _mediator;

        public PeopleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("FindTagPeopleById/{Id}", Name = "FindTagPeopleById")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TagVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TagVm>>> FindTagPeopleById(Guid Id)
        {
            var query = new FindTagPeopleByIdQuery(Id);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }
        [HttpGet("FindPeopleByTagId/{TagId}", Name = "FindPeopleByTagId")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PeopleVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PeopleVm>>> FindPeopleByTagId(Guid TagId)
        {
            var query = new FindPeopleByTagIdQuery(TagId);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }
        [HttpGet("FindPeopleById/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PeopleFullVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PeopleFullVm>>> FindPeopleById(Guid id)
        {
            var query = new FindPeopleByIdQuery(id);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }

        [HttpGet("FindPeopleByNationalId/{nationalId}")]
        [ProducesResponseType(typeof(PeopleFullVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PeopleFullVm>> FindPeopleByNationalId(string nationalId)
        {
            var query = new FindPeopleByNationalIdQuery(nationalId);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }

        [HttpGet("FindUserByNameAsync/{name}", Name = "FindUserByNameAsync")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PeopleVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PeopleVm>>> FindUserByNameAsync(string name)
        {
            var query = new FindPeopleByNameListQuery(name);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }

        // Experimental
        [HttpGet("FindUserByNameLucene/{name}", Name = "FindUserByNameLucene")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PeopleVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PeopleVm>>> FindUserByNameLucene(string name)
        {
            var query = new OnLuceneFindPeopleByNameQuery(name);
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }

        [HttpGet( Name = "GetListPeople")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PeopleFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PeopleFullVm>>> GetListPeople()
        {
            var query = new GetListPeopleQuery();
            var peoples = await _mediator.Send(query);
            return Ok(peoples);
        }
        [HttpPost(Name = "CreatePeople")]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreatePeople([FromBody] CreatePeopleCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(Name = "UpdatePeople")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePeople([FromBody] UpdatePeopleCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeletePeople")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeletePeople(Guid id)
        {
            var command = new DeletePeopleCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("AddTagToPerson", Name = "AddTagToPerson")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddTagToPerson([FromBody] AddTagToPersonCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("DeleteTagToPerson", Name = "DeleteTagToPerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> DeleteTagToPerson([FromBody] DeleteTagToPersonCommand command)
        {
            return await _mediator.Send(command);
        }

        // Experimental
        [HttpPost("IndexLucenePeople", Name = "IndexLucenePeople")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> IndexLucenePeople()
        {
            var query = new IndexLucenePeopleCommand();
            await _mediator.Send(query);
            return Ok();
        }

    }
}
