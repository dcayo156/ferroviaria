using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Mails.Commands.DeleteMail
{
    public class DeleteMailCommandHandler : IRequestHandler<DeleteMailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteMailCommandHandler> _logger;

        public DeleteMailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteMailCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteMailCommand request, CancellationToken cancellationToken)
        {
            var mailToDelete = await _unitOfWork.Repository<CommunicationChannel>().GetByIdAsync(request.Id);
            if (mailToDelete == null)
            {
                _logger.LogError($"{request.Id} Mail no existe en el sistema");
              throw new NotFoundException(nameof(Addresses), request.Id);
            }
            _unitOfWork.Repository<CommunicationChannel>().DeleteEntity(mailToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} Mail fue eliminado con exito");

            return Unit.Value;


        }
    }
}
