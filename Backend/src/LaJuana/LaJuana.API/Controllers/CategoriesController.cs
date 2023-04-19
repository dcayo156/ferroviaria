using LaJuana.Application.Features.Categories.Commands.CreateCategories;
using LaJuana.Application.Features.Categories.Commands.DeleteCategories;
using LaJuana.Application.Features.Categories.Commands.UpdateCategories;
using LaJuana.Application.Features.Categories.Queries.FindCategoriesById;
using LaJuana.Application.Features.Categories.Queries.GetListCategories;
using LaJuana.Application.Features.Categories.Queries.GetListChildrenCategories;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateCategories")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateCategories([FromBody] CreateCategoriesCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut("UpdateCategories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize]
        public async Task<ActionResult> UpdateCategories([FromBody] UpdateCategoriesCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeleteCategories/{id}", Name = "DeleteCategories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCategories(Guid id)
        {
            var command = new DeleteCategoriesCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
        [HttpGet("GetCategories")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<CategoriesFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoriesFullVm>>> GetCategoriess()
        {
            var query = new GetListCategoriesQuery();
            var Categoriess = await _mediator.Send(query);
            return Ok(Categoriess);
        }
        [HttpGet("GetCategoriesChildren")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<CategoriesChildrenFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoriesChildrenFullVm>>> GetCategoriesChildren()
        {
            var query = new GetListChildrenCategoriesQuery();
            var Categoriess = await _mediator.Send(query);
            return Ok(Categoriess);
        }
        [HttpGet("FindCategoriesById/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoriesFullVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoriesFullVm>>> FindCategoriesById(Guid id)
        {
            var query = new FindCategoriesByIdQuery(id);
            var Categories = await _mediator.Send(query);
            return Ok(Categories);
        }
    }
}
