using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Peoples.Commands.DeletePeople
{
    public class DeletePeopleCommandHandler : IRequestHandler<DeletePeopleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IPeopleRepository _PeopleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePeopleCommandHandler> _logger;

        public DeletePeopleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeletePeopleCommandHandler> logger)
        {
            //_PeopleRepository = PeopleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeletePeopleCommand request, CancellationToken cancellationToken)
        {
            var peopleToDelete = await _unitOfWork.PeopleRepository.GetByIdAsync(request.Id);
            if (peopleToDelete == null)
            {
                _logger.LogError($"{request.Id} people no existe en el sistema");
                throw new NotFoundException(nameof(People), request.Id);      
            }

            //await _PeopleRepository.DeleteAsync(peopleToDelete);
            _unitOfWork.PeopleRepository.DeleteEntity(peopleToDelete);
            
            if(await _unitOfWork.Complete() > 0){
                _unitOfWork.PeopleRepository.DeleteEntityLucene(peopleToDelete);
            }
            
            _logger.LogInformation($"El {request.Id} people fue eliminado con exito");

            return Unit.Value;
        }
    }
}
