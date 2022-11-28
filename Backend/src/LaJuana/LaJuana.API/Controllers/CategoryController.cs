using LaJuana.Application.Features.TagCategories.Commads.CreateTagCategory;
using LaJuana.Application.Features.TagCategories.Commads.DeleteTagCategory;
using LaJuana.Application.Features.TagCategories.Commads.UpdateTagCategory;
using LaJuana.Application.Features.TagCategories.Queries.GetTagCategoryList;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCategories")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TagCategoryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TagCategoryVm>>> GetCategories()
        {
            var query = new GetTagCategoryListQuery();
            var tags = await _mediator.Send(query);
            return Ok(tags);
        }
        [HttpPost("CreateTagCategory")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateTagCategory([FromBody] CreateTagCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdateTagCategory")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateTag([FromBody] UpdateTagCategoryCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
        [HttpDelete("Delete/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteTag(Guid id)
        {
            var command = new DeleteTagCategoryCommand
            {
                Id = id
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
