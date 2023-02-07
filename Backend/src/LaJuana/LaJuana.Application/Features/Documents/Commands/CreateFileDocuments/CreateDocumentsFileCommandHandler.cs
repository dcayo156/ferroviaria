using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using LaJuana.Application.Models.ViewModels;

namespace LaJuana.Application.Features.Documents.Commands.CreateFileDocuments
{
    public class CreateDocumentsFileCommandHandler : IRequestHandler<CreateDocumentsFileCommand, FileDirectoryResponseVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDocumentsFileCommandHandler> _logger;
        private readonly IDocumentService _documentService;
        public CreateDocumentsFileCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateDocumentsFileCommandHandler> logger,
            IDocumentService documentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _documentService = documentService;
        }

        async Task<FileDirectoryResponseVm> IRequestHandler<CreateDocumentsFileCommand, FileDirectoryResponseVm>.Handle(CreateDocumentsFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //if (DocumentsToUpdate == null) 
                //{
                //    _logger.LogError($"No se encontro el Document id {request.Id}");
                //    throw new NotFoundException(nameof(Document), request.Id);
                //}            
                if (request.FileName == string.Empty) throw new Exception("File name se encuentra vacio");

                var pathFile = await _documentService.SaveDocument(
                                 request.FilePath,
                                 request.FileName,
                                 request.File,
                                 request.IsFile,
                                 true);
                FileDirectoryResponseVm res= new FileDirectoryResponseVm() {
                    FileName= request.FileName,
                    FilePath= pathFile
                };
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
            //if (request.IsFile)
            //{
            //    DocumentsToUpdate.FileName = request.FileName;
            //    DocumentsToUpdate.FilePath = pathFile;
            //}
            //else
            //{
            //    DocumentsToUpdate.PhotoName = request.FileName;
            //    DocumentsToUpdate.PhotoPath = pathFile;
            //}

            //_unitOfWork.Repository<Document>().UpdateEntity(DocumentsToUpdate);

            //await _unitOfWork.Complete();

            //_logger.LogInformation($"La operacion fue exitosa actualizando el Documents {request.Id}");

            //return DocumentsToUpdate.Id;
        }
    }
}
