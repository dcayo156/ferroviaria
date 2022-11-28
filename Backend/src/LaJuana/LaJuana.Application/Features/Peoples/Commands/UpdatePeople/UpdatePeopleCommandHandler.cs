using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LaJuana.Application.Features.Peoples.Commands.UpdatePeople
{
    public class DeletePeopleCommandHandler : IRequestHandler<UpdatePeopleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IPeopleRepository _PeopleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePeopleCommandHandler> _logger;

        public DeletePeopleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, Contracts.Infrastructure.IEmailService @object, ILogger<DeletePeopleCommandHandler> logger)
        {
            //_PeopleRepository = PeopleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdatePeopleCommand request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<People, object>>>();
            includes.Add(x => x.Addresses!);
            includes.Add(x => x.Tags!);
            includes.Add(x => x.CommunicationChannels!);
            var people = await _unitOfWork.PeopleRepository.GetAsync(a => a.Id == request.Person!.Id, null , includes:includes, false);
            var peopleToUpdate = people.FirstOrDefault();

            if (peopleToUpdate == null)
            {
                _logger.LogError($"No se encontro el People id {request.Person!.Id}");
                throw new NotFoundException(nameof(Organization), request.Person!.Id);
            }

            var peopleEntity = _mapper.Map<People>(request.Person);
            var listMailEntity = _mapper.Map<List<Mail>>(request.Person!.Mails);
            var listPhoneEntity = _mapper.Map<List<Phone>>(request.Person.Phones);
            var listAddressEntity = _mapper.Map<List<Address>>(request.Person.Addresses);

            foreach (var idTag in request.Tags!)
            {
                var tagEntity = (await _unitOfWork.Repository<Tag>().GetAsync(m => m.Id == idTag)).FirstOrDefault();
                if (tagEntity == null)
                {
                    throw new Exception($"El tag con id: " + idTag + " no se encuentra registrado");
                }

                var a = idTag.ToString().ToUpper();
                var existTag = peopleToUpdate.Tags.Count(tag => tag.Id == idTag) == 0;
                if (existTag)
                {
                    peopleToUpdate.Tags.Add(tagEntity);
                }
                else
                {
                    peopleToUpdate.Tags.Remove(tagEntity);
                }
            }

            foreach (var mailEntity in listMailEntity)
            {
                var mail = (await _unitOfWork.Repository<Mail>().GetAsync(m => m.Id == mailEntity.Id)).FirstOrDefault();

                if (mail == null)
                {
                    throw new Exception($"El mail con id: " + mailEntity.Id + " no se encuentra registrado");
                }
                var mailExist = peopleToUpdate.CommunicationChannels.Count(mail => mail.Id == mailEntity.Id) == 0;
                if (mailExist)
                {
                    mail.Email = mailEntity.Email;
                    peopleToUpdate.CommunicationChannels.Add(mail);
                }
                else
                {
                    peopleToUpdate.CommunicationChannels.Remove(mail);
                }
            }
            foreach (var phoneEntity in listPhoneEntity)
            {
                var phone = (await _unitOfWork.Repository<Phone>().GetAsync(m => m.Id == phoneEntity.Id)).FirstOrDefault();

                if (phone == null)
                {
                    throw new Exception($"El phone con id: " + phoneEntity.Id + " no se encuentra registrado");
                }
                var phoneExist = peopleToUpdate.CommunicationChannels.Count(mail => mail.Id == phoneEntity.Id) == 0;
                if (phoneExist)
                {
                    phone.PhoneNumber = phoneEntity.PhoneNumber;
                    peopleToUpdate.CommunicationChannels.Add(phone);
                }
                else
                {
                    peopleToUpdate.CommunicationChannels.Remove(phone);
                }
            }
            //Copiar los valores de propiedades de People entity a peopleUpdate
            //Por cada una de las colecciones de las lista que tienen 
            //1: Recorrer la lista PeopleEntity y por cada elemento buscarlo en la lista de PeopleUptade, si no esta Agregarlo a la lista de PeopleUpdate
            //2: Recorrer cada elemento de lista de Elemento de PeopleUpdate y buscarlo en las lista correspondiente de PeopleEntity, si en esta no esta Eliminarlo de PeopleUpdate
            //3: Recorrer cada elemento de PeopleEntity buscar el mismo elemento en PeopleUpdate y copiarle el valor de las propiedades

            _unitOfWork.PeopleRepository.UpdateEntity(peopleEntity);

            if (await _unitOfWork.Complete() > 0)
            {
                _unitOfWork.PeopleRepository.UpdateEntityLucene(peopleEntity);
            }
            _logger.LogInformation($"La operacion fue exitosa actualizando el people {request.Person.Id}");

            return Unit.Value;
        }
    }
}
