using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAddressCommand> _logger;
        public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateAddressCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            
            var addressToUpdate = await _unitOfWork.Repository<Address>().GetByIdAsync(request.Id);
            if (addressToUpdate == null)
            {
                _logger.LogError($"No se encontro el Adress id {request.Id}");
                throw new NotFoundException(nameof(Address), request.Id);
            }
            _mapper.Map(request, addressToUpdate, typeof(UpdateAddressCommand), typeof(Address));
             
            var personToUpdate = await _unitOfWork.Repository<Person>().GetByIdAsync(request.PersonId);

            if (personToUpdate == null)
            {
                _logger.LogError($"No se encontro el Person id {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }

            _unitOfWork.Repository<Address>().UpdateEntity(addressToUpdate);
            await _unitOfWork.Complete();
            _logger.LogInformation($"La operacion fue exitosa actualizando el Address {request.Id}");
            return Unit.Value;
        }
    }
}
