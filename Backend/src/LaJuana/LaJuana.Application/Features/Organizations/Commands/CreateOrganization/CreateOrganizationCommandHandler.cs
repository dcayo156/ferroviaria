using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Organizations.Commands.CreateOrganization
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailservice;
        private readonly ILogger<CreateOrganizationCommandHandler> _logger;

        // Constructor
        public CreateOrganizationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailservice, ILogger<CreateOrganizationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailservice = emailservice;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organizationEntity = _mapper.Map<Organization>(request.Person);
            var listMailEntity = _mapper.Map<List<Mail>>(request.Person!.Mails);
            var listPhoneEntity = _mapper.Map<List<Phone>>(request.Person.Phones);

            foreach (var idTag in request.Tags!)
            {
                var tagExist = (await _unitOfWork.Repository<Tag>().GetAsync(m => m.Id == idTag)).FirstOrDefault();

                if (tagExist == null)
                {
                    throw new Exception($"El tag con id: " + idTag + " no se encuentra registrado");
                }

                organizationEntity.Tags.Add(tagExist);
            }

            foreach (var mailEntity in listMailEntity)
            {
                organizationEntity.CommunicationChannels.Add(mailEntity);
            }
            foreach (var phoneEntity in listPhoneEntity)
            {
                organizationEntity.CommunicationChannels.Add(phoneEntity);
            }

            // Save
            _unitOfWork.OrganizationRepository.AddEntity(organizationEntity);

            var result = await _unitOfWork.Complete();

            return organizationEntity.Id;

        }
    }
}
