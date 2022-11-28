using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Queries.FindPeopleByTagId
{
    public class FindPeopleByTagIdQueryHandler : IRequestHandler<FindPeopleByTagIdQuery, List<PeopleVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public FindPeopleByTagIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public  async Task<List<PeopleVm>> Handle(FindPeopleByTagIdQuery request, CancellationToken cancellationToken)
        {           
            var TagUserList = await _unitOfWork.PeopleRepository.FindByTagIdAsync<People>(request.TagId);
            return _mapper.Map<List<PeopleVm>>(TagUserList);
        }
    }
}
