using System.Linq;
using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Application.Models;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Mails.Commands.CreateMail
{
    public class CreateMailCommandHandler : IRequestHandler<CreateMailCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailservice;
        private readonly ILogger<CreateMailCommandHandler> _logger;

        // Constructor
        public CreateMailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailservice, ILogger<CreateMailCommandHandler> logger)
        {
            //_CommunicationChannelsRepository = CommunicationChannelsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailservice = emailservice;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateMailCommand request, CancellationToken cancellationToken)
        {
            var mailEntity = _mapper.Map<Mail>(request);

            var personToInsert = await _unitOfWork.Repository<Person>().GetByIdAsync(request.PersonId);

            if (personToInsert == null)
            {
                _logger.LogError($"No se encontro el Person id {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }
            _unitOfWork.Repository<CommunicationChannel>().AddEntity(mailEntity);

            var result = await _unitOfWork.Complete();

            return mailEntity.Id;

        }
    }
}
