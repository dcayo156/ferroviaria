using LaJuana.Application.Features.Documents.Commands.CreateDocuments;
using LaJuana.Application.Features.Documents.Commands.CreateFileDocuments;
using LaJuana.Application.Features.Documents.Commands.DeleteDocuments;
using LaJuana.Application.Features.Documents.Commands.UpdateDocuments;
using LaJuana.Application.Features.Documents.Queries.FindDocumentsById;
using LaJuana.Application.Features.Documents.Queries.FindDocumentsFileById;
using LaJuana.Application.Features.Documents.Queries.GetListDocuments;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateDocuments")]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> CreateDocuments([FromBody] CreateDocumentsCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("CreateDocumentsFile")]
        //[Authorize]
        public async Task<ActionResult<FileDirectoryResponseVm>> CreateDocumentsFile([FromBody] CreateDocumentsFileCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut("UpdateDocuments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateDocuments([FromBody] UpdateDocumentsCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeleteDocuments/{id}", Name = "DeleteDocuments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteDocuments(Guid id)
        {
            var command = new DeleteDocumentsCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
        [HttpGet("GetDocuments")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<DocumentsFullVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DocumentsFullVm>>> GetDocumentss()
        {
            var query = new GetListDocumentsQuery();
            var Documentss = await _mediator.Send(query);
            return Ok(Documentss);
        }
        [HttpGet("FindDocumentsById/{id}")]
        //[Authorize]
        [ProducesResponseType(typeof(DocumentsFullVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DocumentsFullVm>>> FindDocumentsById(Guid id)
        {
            var query = new FindDocumentsByIdQuery(id);
            var Documents = await _mediator.Send(query);
            return Ok(Documents);
        }

        [HttpGet("FindDocumentsFileById/{id}")]
        public async Task<ActionResult<string>> FindDocumentsFileById(string id, bool isFile)
        {
            FindDocumentsFileByIdQuery command = new FindDocumentsFileByIdQuery(new Guid(id), isFile);
            var documentFileVm = await _mediator.Send(command);
            return File(System.IO.File.OpenRead(documentFileVm.FilePath), documentFileVm.MimeType, documentFileVm.FileName);
        }
    }
}
