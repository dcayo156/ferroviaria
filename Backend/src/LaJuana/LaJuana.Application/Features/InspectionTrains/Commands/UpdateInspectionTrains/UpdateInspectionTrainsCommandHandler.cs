using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.InspectionTrains.Commands.UpdateInspectionTrains
{
    public class UpdateInspectionTrainsCommandHandler : IRequestHandler<UpdateInspectionTrainsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInspectionTrainsCommand> _logger;
        private readonly IDocumentService _documentService;
        private readonly IAposeService _aposeService;
        public UpdateInspectionTrainsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateInspectionTrainsCommand> logger,
            IDocumentService documentService,
            IAposeService aposeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _documentService = documentService;
            _aposeService = aposeService;
        }

        public async Task<Unit> Handle(UpdateInspectionTrainsCommand request, CancellationToken cancellationToken)
        {
            await Validation(request);

            var inspectionTrainsToUpdate = await _unitOfWork.Repository<InspectionTrain>().GetByIdAsync(request.Id);
            if (inspectionTrainsToUpdate == null)
            {
                _logger.LogError($"No se encontro el InspectionTrain id {request.Id}");
                throw new NotFoundException(nameof(InspectionTrain), request.Id);
            }

            var pathFile = await _documentService.SaveDocument(
                                 request.SubCategoryId,
                                 inspectionTrainsToUpdate.FileName,
                                 request.File,
                                 true,
                                 false);

            inspectionTrainsToUpdate = await _aposeService.ReadDocInspectionIntegral(pathFile, inspectionTrainsToUpdate);

            _unitOfWork.Repository<InspectionTrain>().UpdateEntity(inspectionTrainsToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el InspectionTrains {request.Id}");

            return Unit.Value;
        }
        public async Task Validation(UpdateInspectionTrainsCommand request)
        {
            if (request == null) { throw new Exception("El objeto para dar alta la Categoria es null"); }

            if (request.SubCategoryId == null)
            {
                _logger.LogError($"CategoriaId o SubCategoriaId en null");
                throw new Exception($"CategoriaId o SubCategoriaId en null");
            }
            var subCategory = await _unitOfWork.CategoryRepository.FindByIdAsync(Guid.Parse(request.SubCategoryId));
            if (subCategory == null)
            {
                _logger.LogError($"No se encontro la SubCategoriaId {request.SubCategoryId}");
                throw new Exception($"No se encontro la SubCategiaId {request.SubCategoryId}");
            }
            if (request.FileName == string.Empty) throw new Exception("File name se encuentra vacio");
        }
    }
}
