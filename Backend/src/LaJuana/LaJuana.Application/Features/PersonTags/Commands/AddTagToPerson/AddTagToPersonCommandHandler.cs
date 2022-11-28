using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using LaJuana.Application.Exceptions;


namespace LaJuana.Application.Features.PersonTags.Commands
{
    public class AddTagToPersonCommandHandler : IRequestHandler<AddTagToPersonCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddTagToPersonCommandHandler> _logger;
        private readonly IMapper _mapper;
        public AddTagToPersonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddTagToPersonCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        private async Task<People> GetPeople(Guid personId)
        {
            People people = await _unitOfWork.PeopleRepository.FindByIdAsync(personId);
            return people;
        }
        private async Task<bool> ExistTag(Guid tagId)
        {
            Tag tag = await _unitOfWork.TagRepository.FindTagByIdAsync(tagId);
            return tag != null;
        }
        public async Task<Guid> Handle(AddTagToPersonCommand request, CancellationToken cancellationToken)
        {
            People p = await this.GetPeople(request.PersonId);
            
            if(p==null){
                throw new NotFoundException(nameof(People), request.PersonId);
            }
            if(!(await ExistTag(request.TagId))){
                throw new Exception("El tag con ID:"+request.TagId+" no ha sido encontrado");
            }
            if(p.Tags.Where(t => t.Id == request.TagId).Any()){
                throw new Exception("El tag con ID:"+request.TagId+" ya existe para la persona");
            }
            var personTag = _mapper.Map<PersonTag>(request);
            _unitOfWork.Repository<PersonTag>().AddEntity(personTag);
            var result = await _unitOfWork.Complete();
            
            return personTag.Id;
        }
    }
}
