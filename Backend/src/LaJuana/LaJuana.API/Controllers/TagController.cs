using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LaJuana.Application.Features.Tags.Commads.CreateTag;
using LaJuana.Application.Features.Tags.Commads.DeleteTag;
using LaJuana.Application.Features.Tags.Commads.UpdateTag;
using LaJuana.Application.Features.Tags.Queries.FindTagById;
using LaJuana.Application.Features.Tags.Queries.FindTagByName;
using LaJuana.Application.Features.Tags.Queries.FindTagByTagCategoryId;
using LaJuana.Application.Features.Tags.Queries.GetTagsList;
using LaJuana.Application.Features.Tags.Queries.GetTagWithAddressesWithinMap;
using LaJuana.Application.Features.Tags.Queries.GetTagsWithAddressCountOfPerson;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("FindTagById/{id}")]
        //[Authorize]
        [ProducesResponseType(typeof(TagFullVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TagFullVm>>> FindTagById(Guid id)
        {
            var query = new FindTagByIdQuery(id);
            var tag = await _mediator.Send(query);
            return Ok(tag);
        }

        [HttpGet("FindTagByName/{name}")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<TagFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TagFullVm>>> FindTagByName(string name)
        {
            var query = new FindTagByNameQuery(name);
            var tags = await _mediator.Send(query);
            return Ok(tags);
        }

        [HttpGet("FindTagByCategoryId/{categoryId}")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<TagFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TagFullVm>>> FindTagByCategoryId(Guid categoryId)
        {
            var query = new FindTagByTagCategoryIdQuery(categoryId);
            var tags = await _mediator.Send(query);
            return Ok(tags);
        }

        [HttpGet("GetTags")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<TagFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TagFullVm>>> GetTags()
        {
            var query = new GetTagsListQuery();
            var tags = await _mediator.Send(query);
            return Ok(tags);
        }
        [HttpPost("GetTagWithAddressesWithinMapBound")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TagWithPeopleAddressesVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PersonAddress>>> GetTagsFromLucene([FromBody]GetTagWithAddressesWithinMapQuery query)
        {
            var tags = await _mediator.Send(query);
            return Ok(tags);
        }

        [HttpGet("GetTagsWithAddressCountOfPerson")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<CategoryWithTagsVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoryWithTagsVm>>> GetTagsWithAddressCountOfPerson()
        {
            var query = new GetTagsWithAddressCountOfPersonQuery();
            var tags = await _mediator.Send(query);
            return Ok(tags);
        }


        [HttpPost("CreateTag")]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateTag([FromBody] CreateTagCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdateTag")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateTag([FromBody] UpdateTagCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeleteTag/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteTag(Guid id)
        {
            var command = new DeleteTagCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }

    }
}
