using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Queries.FindTagPeopleById
{
    internal class FindTagPeopleByIdQueryHandler : IRequestHandler<FindTagPeopleByIdQuery, List<TagVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public FindTagPeopleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagVm>> Handle(FindTagPeopleByIdQuery request, CancellationToken cancellationToken)
        {
            var people = await _unitOfWork.PeopleRepository.FindByIdAsync(request.Id);
            var tags = people?.Tags;

            return _mapper.Map<List<TagVm>>(tags);            
        }
    }
}
