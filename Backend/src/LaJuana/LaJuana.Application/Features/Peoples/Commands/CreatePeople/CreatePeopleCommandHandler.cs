using System.Linq;
using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Peoples.Commands
{
    public class CreatePeopleCommandHandler : IRequestHandler<CreatePeopleCommand, Guid>
    {
        //private readonly IPeopleRepository _PeopleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailservice;
        private readonly ILogger<CreatePeopleCommandHandler> _logger;

        // Constructor
        public CreatePeopleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailservice, ILogger<CreatePeopleCommandHandler> logger)
        {
            //_PeopleRepository = PeopleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailservice = emailservice;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreatePeopleCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var peopleEntity = _mapper.Map<People>(request.Person);
                var listMailEntity = _mapper.Map<List<Mail>>(request.Person!.Mails);
                var listPhoneEntity = _mapper.Map<List<Phone>>(request.Person.Phones);
                
                //var listTagEntity = _mapper.Map<List<Tag>>(request.Tags);
                
                foreach (var idTag in request.Tags!)
                {
                    var tagExist = (await _unitOfWork.Repository<Tag>().GetAsync(m => m.Id == idTag)).FirstOrDefault();

                    if (tagExist == null)
                    {
                        throw new Exception($"El tag con id: " + idTag + " no se encuentra registrado");
                    }

                    peopleEntity.Tags.Add(tagExist);
                }

                foreach (var mailEntity in listMailEntity)
                {
                    peopleEntity.CommunicationChannels.Add(mailEntity);
                }
                foreach (var phoneEntity in listPhoneEntity)
                {
                    peopleEntity.CommunicationChannels.Add(phoneEntity);
                }

                // Save
                _unitOfWork.PeopleRepository.AddEntity(peopleEntity);
                
                var result = await _unitOfWork.Complete();
                if(result > 0){
                    _unitOfWork.PeopleRepository.AddEntityLucene(peopleEntity);
                    foreach(Tag tag in peopleEntity.Tags){
                        _unitOfWork.TagRepository.AddPersonToTagInLucene(peopleEntity,tag);
                    }
                }
                //await SendEmail(peopleEntity);

                return peopleEntity.Id;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }

        }

        private async Task SendEmail(People people)
        {
            var email = new Email
            {
                To = "LaJuana@gmail.com",
                Body = "La compania de people se creo correctamente",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailservice.SendEmail(email);
            }
            catch (Exception ex) {
                _logger.LogError($"Errores enviando el email de {people.Id}");
            }

        }

    }
}
