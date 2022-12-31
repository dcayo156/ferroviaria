using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Documents.Commands.DeleteDocuments
{
    public class DeleteDocumentsCommandHandler : IRequestHandler<DeleteDocumentsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDocumentsCommandHandler> _logger;

        public DeleteDocumentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteDocumentsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDocumentsCommand request, CancellationToken cancellationToken)
        {
            var DocumentsToDelete = await _unitOfWork.Repository<Document>().GetByIdAsync(request.Id);
            if (DocumentsToDelete == null)
            {
                _logger.LogError($"{request.Id} Document no existe en el sistema");
                throw new NotFoundException(nameof(Document), request.Id);
            }
            DocumentsToDelete.Status = (int)DocumentStatus.Deshabilitado;
            _unitOfWork.Repository<Document>().UpdateEntity(DocumentsToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} Documents fue eliminado con exito");

            return Unit.Value;
        }
    }
}