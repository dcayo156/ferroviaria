using System.Linq;
using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Application.Models;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Phones.Commands.CreatePhone
{
    public class CreatePhoneCommandHandler : IRequestHandler<CreatePhoneCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _ePhoneservice;
        private readonly ILogger<CreatePhoneCommandHandler> _logger;

        // Constructor
        public CreatePhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService ePhoneservice, ILogger<CreatePhoneCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ePhoneservice = ePhoneservice;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreatePhoneCommand request, CancellationToken cancellationToken)
        {
            var phoneEntity = _mapper.Map<Phone>(request);

            var personToInsert = await _unitOfWork.Repository<Person>().GetByIdAsync(request.PersonId);

            if (personToInsert == null)
            {
                _logger.LogError($"No se encontro el Person id {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }
            _unitOfWork.Repository<CommunicationChannel>().AddEntity(phoneEntity);

            var result = await _unitOfWork.Complete();

            return phoneEntity.Id;

        }
    }
}
