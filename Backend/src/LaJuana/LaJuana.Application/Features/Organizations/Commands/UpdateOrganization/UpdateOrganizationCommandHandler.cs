using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Organizations.Commands.UpdateOrganization
{
    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrganizationCommandHandler> _logger;
        public UpdateOrganizationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateOrganizationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organizationToUpdate = await _unitOfWork.OrganizationRepository.GetByIdAsync(request.Id);

            if (organizationToUpdate == null)
            {
                _logger.LogError($"No se encontro el Organization id {request.Id}");
                throw new NotFoundException(nameof(Organization), request.Id);
            }

            _mapper.Map(request, organizationToUpdate, typeof(UpdateOrganizationCommand), typeof(Organization));


            _unitOfWork.OrganizationRepository.UpdateEntity(organizationToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el Organization {request.Id}");

            return Unit.Value;
        }
    }
}
