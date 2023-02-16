using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Documents.Commands.CreateDocuments
{
    public class CreateDocumentsCommandHandler : IRequestHandler<CreateDocumentsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDocumentsCommand> _logger;
        private readonly IDocumentService _documentService;
        public CreateDocumentsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateDocumentsCommand> logger,
            IDocumentService documentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _documentService = documentService;
        }

        public async Task<Guid> Handle(CreateDocumentsCommand request, CancellationToken cancellationToken)
        {
            await Validation(request);

            var documentEntity = _mapper.Map<Document>(request);
            
            documentEntity.Status = (int)DocumentStatus.Habilitado;

            var countDocuemnt = await _unitOfWork.DocumentRepository.GetCountDocuments() + 1;

            documentEntity.Code = DateTime.Now.ToString("MM/dd/yyyy H:mm") + "-" + countDocuemnt.ToString();

            _unitOfWork.Repository<Document>().AddEntity(documentEntity);

            await _unitOfWork.Complete();

            return documentEntity.Id;
        }
        public async Task Validation(CreateDocumentsCommand request)
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
