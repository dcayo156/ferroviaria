using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Mails.Commands.UpdateMail
{
    public class UpdateMailCommandHandler : IRequestHandler<UpdateMailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateMailCommandHandler> _logger;
        public UpdateMailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateMailCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateMailCommand request, CancellationToken cancellationToken)
        {
            var mailToUpdate = await _unitOfWork.Repository<CommunicationChannel>().GetByIdAsync(request.Id);
            if (mailToUpdate == null)
            {
                _logger.LogError($"No se encontro el Mail id {request.Id}");
                throw new NotFoundException(nameof(Mail), request.Id);
            }
            _mapper.Map(request, mailToUpdate, typeof(UpdateMailCommand), typeof(UpdateMailCommand));
             
            var personToUpdate = await _unitOfWork.Repository<Person>().GetByIdAsync(request.PersonId);
            if (personToUpdate == null)
            {
                _logger.LogError($"No se encontro el Person id {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }

            _unitOfWork.Repository<CommunicationChannel>().UpdateEntity(mailToUpdate);
            await _unitOfWork.Complete();
            _logger.LogInformation($"La operacion fue exitosa actualizando el Mail {request.Id}");
            return Unit.Value;
        }
    }
}
