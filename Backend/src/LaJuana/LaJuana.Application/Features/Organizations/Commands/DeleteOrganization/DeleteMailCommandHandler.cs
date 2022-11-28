using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Organizations.Commands.DeleteOrganization
{
    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrganizationCommandHandler> _logger;

        public DeleteOrganizationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteOrganizationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organizationToDelete = await _unitOfWork.OrganizationRepository.GetByIdAsync(request.Id);
            if (organizationToDelete == null)
            {
                _logger.LogError($"{request.Id} Organization no existe en el sistema");
              throw new NotFoundException(nameof(Organization), request.Id);
            }

            _unitOfWork.OrganizationRepository.DeleteEntity(organizationToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} Organization fue eliminado con exito");

            return Unit.Value;
        }
    }
}
