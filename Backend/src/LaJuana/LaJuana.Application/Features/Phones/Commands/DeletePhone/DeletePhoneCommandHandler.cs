using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Phones.Commands.DeletePhone
{
    public class DeletePhoneCommandHandler : IRequestHandler<DeletePhoneCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePhoneCommandHandler> _logger;

        public DeletePhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeletePhoneCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeletePhoneCommand request, CancellationToken cancellationToken)
        {
            var phoneToDelete = await _unitOfWork.Repository<CommunicationChannel>().GetByIdAsync(request.Id);
            if (phoneToDelete == null)
            {
                _logger.LogError($"{request.Id} Phone no existe en el sistema");
              throw new NotFoundException(nameof(Addresses), request.Id);
            }
            _unitOfWork.Repository<CommunicationChannel>().DeleteEntity(phoneToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} Phone fue eliminado con exito");

            return Unit.Value;


        }
    }
}
