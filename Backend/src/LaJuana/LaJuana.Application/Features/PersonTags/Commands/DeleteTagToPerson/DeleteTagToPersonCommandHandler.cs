using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace LaJuana.Application.Features.PersonTags.Commands
{
    public class DeleteTagToPersonCommandHandler : IRequestHandler<DeleteTagToPersonCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTagToPersonCommandHandler> _logger;
        private readonly IMapper _mapper;
        public DeleteTagToPersonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteTagToPersonCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        private async Task<People> GetPeople(Guid personId)
        {
            var person = await _unitOfWork.PeopleRepository.FindByIdAsync(personId);
            return person;
        }
        private async Task<PersonTag?> GetTagToRemove(Guid personId, Guid tagId){
            var personTag = await _unitOfWork.PeopleRepository.FindPersonTag(personId,tagId);
            return personTag;
        }
        public async Task<Guid> Handle(DeleteTagToPersonCommand request, CancellationToken cancellationToken)
        {
            if(await this.GetPeople(request.PersonId)==null){
                throw new Exception("La persona con ID:"+request.PersonId+" no ha sido encontrado");
            }
            PersonTag? tagToRemove=await GetTagToRemove(request.PersonId,request.TagId);
            if(tagToRemove==null){
                throw new Exception("La combinacion tag-persona no ha sido encontrada");
            }
            _unitOfWork.Repository<PersonTag>().DeleteEntity(tagToRemove);    
            var result = await _unitOfWork.Complete();
            return tagToRemove.Id;
        }
    }
}
