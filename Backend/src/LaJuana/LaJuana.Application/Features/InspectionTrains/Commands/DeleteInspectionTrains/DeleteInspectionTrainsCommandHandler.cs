using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Application.Features.Documents.Commands.DeleteDocuments;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.InspectionTrains.Commands.DeleteInspectionTrains
{
    public class DeleteInspeccionTrainCommandHandler : IRequestHandler<DeleteInspectionTrainsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteInspeccionTrainCommandHandler> _logger;

        public DeleteInspeccionTrainCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteInspeccionTrainCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteInspectionTrainsCommand request, CancellationToken cancellationToken)
        {
            var InspeccionTrainToDelete = await _unitOfWork.Repository<InspectionTrain>().GetByIdAsync(request.Id);
            if (InspeccionTrainToDelete == null)
            {
                _logger.LogError($"{request.Id} Inpeccion de tren no existe en el sistema");
                throw new NotFoundException(nameof(InspectionTrain), request.Id);
            }
            //InspeccionTrainToDelete.Status = (int)InspeccionTraintatus.Deshabilitado;
            _unitOfWork.Repository<InspectionTrain>().UpdateEntity(InspeccionTrainToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"La {request.Id} Inpeccion de tren fue eliminado con exito");

            return Unit.Value;
        }
    }
}