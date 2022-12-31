using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Application.Features.Documents.Commands.CreateDocuments;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Documents.Commands.UpdateDocuments
{
    public class UpdateDocumentsCommandHandler : IRequestHandler<UpdateDocumentsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDocumentsCommand> _logger; 
        public UpdateDocumentsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateDocumentsCommand> logger)      
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;          
        }      

        public async Task<Unit> Handle(UpdateDocumentsCommand request, CancellationToken cancellationToken)
        {
            await Validation(request);

            var DocumentsToUpdate = await _unitOfWork.Repository<Document>().GetByIdAsync(request.Id);
            if (DocumentsToUpdate == null)
            {
                _logger.LogError($"No se encontro el Document id {request.Id}");
                throw new NotFoundException(nameof(Document), request.Id);  
            }

            _mapper.Map(request, DocumentsToUpdate, typeof(UpdateDocumentsCommand), typeof(Document));

            _unitOfWork.Repository<Document>().UpdateEntity(DocumentsToUpdate);
             
            await _unitOfWork.Complete();
            _logger.LogInformation($"La operacion fue exitosa actualizando el Documents {request.Id}");

            return Unit.Value;
        }
        public async Task Validation(UpdateDocumentsCommand request)
        {
            if (request == null) { throw new Exception("El objeto para dar alta la Categoria es null"); }

            if (request.CategoryId == null || request.SubCategoryId == null)
            {
                _logger.LogError($"CategoriaId o SubCategoriaId en null");
                throw new Exception($"CategoriaId o SubCategoriaId en null");
            }
            var category = await _unitOfWork.CategoryRepository.FindByIdAsync(request.CategoryId.Value);
            if (category == null)
            {
                _logger.LogError($"No se encontro la CategoryId {request.CategoryId.Value}");
                throw new Exception($"No se encontro la CategoryId {request.CategoryId.Value}");
            }
            var subCategory = await _unitOfWork.CategoryRepository.FindByIdAsync(request.SubCategoryId.Value);
            if (subCategory == null)
            {
                _logger.LogError($"No se encontro la SubCategoriaId {request.SubCategoryId.Value}");
                throw new Exception($"No se encontro la SubCategiaId {request.SubCategoryId.Value}");
            }
        }
    }
}
