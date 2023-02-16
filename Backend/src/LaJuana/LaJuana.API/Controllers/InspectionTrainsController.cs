using LaJuana.Application.Features.Documents.Queries.FindDocumentsFileById;
using LaJuana.Application.Features.InspectionTrains.Commands.CreateInspectionTrains;
using LaJuana.Application.Features.InspectionTrains.Commands.UpdateInspectionTrains;
using LaJuana.Application.Features.InspectionTrains.Queries.FindInspectionTrainsFileById;
using LaJuana.Application.Features.InspectionTrains.Queries.GetListInspectionTrainAll;
using LaJuana.Application.Features.InspectionTrains.Queries.GetListInspectionTrains;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class InspectionTrainsController : Controller
    {
        private IMediator _mediator;

        public InspectionTrainsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateInspectionTrains")]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateInspectionTrains([FromBody] CreateInspectionTrainsCommand command)
        {
            return await _mediator.Send(command);
        }
        //[HttpPost("CreateInspectionTrainsFile")]
        ////[Authorize]
        //public async Task<ActionResult<FileDirectoryResponseVm>> CreateInspectionTrainsFile([FromBody] CreateInspectionTrainsFileCommand command)
        //{
        //    return await _mediator.Send(command);
        //}
        [HttpPut("UpdateInspectionTrains")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateInspectionTrains([FromBody] UpdateInspectionTrainsCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        //[HttpDelete("DeleteInspectionTrains/{id}", Name = "DeleteInspectionTrains")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult> DeleteInspectionTrains(Guid id)
        //{
        //    var command = new DeleteInspectionTrainsCommand
        //    {
        //        Id = id
        //    };

        //    await _mediator.Send(command);

        //    return NoContent();
        //}
        [HttpGet("GetInspectionTrains")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<InspectionTrainsFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InspectionTrainsFullVm>>> GetInspectionTrainss()
        {
            var query = new GetListInspectionTrainsQuery();
            var InspectionTrainss = await _mediator.Send(query);
            return Ok(InspectionTrainss);
        }
        //[HttpGet("FindInspectionTrainsById/{id}")]
        ////[Authorize]
        //[ProducesResponseType(typeof(InspectionTrainsFullVm), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<InspectionTrainsFullVm>>> FindInspectionTrainsById(Guid id)
        //{
        //    var query = new FindInspectionTrainsByIdQuery(id);
        //    var InspectionTrains = await _mediator.Send(query);
        //    return Ok(InspectionTrains);
        //}

        [HttpGet("FindInspectionTrainsFileById/{id}")]
        public async Task<ActionResult<string>> FindInspectionTrainsFileById(string id)
        {
            FindInspectionTrainsFileByIdQuery command = new FindInspectionTrainsFileByIdQuery(new Guid(id));
            var InspectionTrainFileVm = await _mediator.Send(command);
            return File(System.IO.File.OpenRead(InspectionTrainFileVm.FilePath), InspectionTrainFileVm.MimeType, InspectionTrainFileVm.FileName);
        }

        [HttpGet("GetInspectionTrainsAll")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<InspectionTrainsFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InspectionTrainsFullVm>>> GetInspectionTrainsAll()
        {
            GetListInspectionTrainAllQuery command = new GetListInspectionTrainAllQuery();
            var InspectionTrainss = await _mediator.Send(command);
            return File(System.IO.File.OpenRead(InspectionTrainss.FilePath), InspectionTrainss.MimeType, InspectionTrainss.FileName);
        }
    }
}
