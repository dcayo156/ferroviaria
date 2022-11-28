using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAddressCommand> _logger;

        public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAddressCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var addressEntity = _mapper.Map<Address>(request);

            var personToInsert = await _unitOfWork.Repository<Person>().GetByIdAsync(request.PersonId);

            if (personToInsert == null)
            {
                _logger.LogError($"No se encontro el Person id {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }
            _unitOfWork.Repository<Address>().AddEntity(addressEntity);

            var result = await _unitOfWork.Complete();

            return addressEntity.Id;

        }
    }
}
