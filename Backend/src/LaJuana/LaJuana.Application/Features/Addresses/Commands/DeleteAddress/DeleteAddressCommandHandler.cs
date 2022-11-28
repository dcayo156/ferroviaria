using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IPeopleRepository _PeopleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAddressCommandHandler> _logger;

        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var addressToDelete = await _unitOfWork.Repository<Address>().GetByIdAsync(request.Id);
            if (addressToDelete == null)
            {
                _logger.LogError($"{request.Id} Address no existe en el sistema");
              throw new NotFoundException(nameof(Addresses), request.Id);
            }
            _unitOfWork.Repository<Address>().DeleteEntity(addressToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} Address fue eliminado con exito");

            return Unit.Value;


        }
    }
}
