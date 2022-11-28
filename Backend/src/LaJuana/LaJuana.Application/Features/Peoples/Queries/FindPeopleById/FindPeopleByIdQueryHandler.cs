using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Queries.FindPeopleById
{
    public class FindPeopleByIdQueryHandler : IRequestHandler<FindPeopleByIdQuery, PeopleFullVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public FindPeopleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PeopleFullVm> Handle(FindPeopleByIdQuery request, CancellationToken cancellationToken)
        {
            var people = await _unitOfWork.PeopleRepository.FindByIdAsync(request.Id);

            return _mapper.Map<PeopleFullVm>(people);

        }
    }
}
