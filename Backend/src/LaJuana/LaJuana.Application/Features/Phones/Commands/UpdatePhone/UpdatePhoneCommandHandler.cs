using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Phones.Commands.UpdatePhone
{
    public class UpdatePhoneCommandHandler : IRequestHandler<UpdatePhoneCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePhoneCommandHandler> _logger;
        public UpdatePhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdatePhoneCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdatePhoneCommand request, CancellationToken cancellationToken)
        {
            var phoneToUpdate = await _unitOfWork.Repository<CommunicationChannel>().GetByIdAsync(request.Id);
            if (phoneToUpdate == null)
            {
                _logger.LogError($"No se encontro el Phone id {request.Id}");
                throw new NotFoundException(nameof(Phone), request.Id);
            }
            _mapper.Map(request, phoneToUpdate, typeof(UpdatePhoneCommand), typeof(Phone));
             
            var personToUpdate = await _unitOfWork.Repository<Person>().GetByIdAsync(request.PersonId);
            if (personToUpdate == null)
            {
                _logger.LogError($"No se encontro el Person id {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }

            _unitOfWork.Repository<CommunicationChannel>().UpdateEntity(phoneToUpdate);
            await _unitOfWork.Complete();
            _logger.LogInformation($"La operacion fue exitosa actualizando el Phone {request.Id}");
            return Unit.Value;
        }
    }
}
