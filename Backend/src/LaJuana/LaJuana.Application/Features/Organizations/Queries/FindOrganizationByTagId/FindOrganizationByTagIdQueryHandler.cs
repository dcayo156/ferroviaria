using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;

namespace LaJuana.Application.Features.Organizations.Queries.FindOrganizationByTagId
{
    internal class FindOrganizationByTagIdQueryHandler : IRequestHandler<FindOrganizationByTagIdQuery, List<OrganizationVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        public FindOrganizationByTagIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<OrganizationVm>> Handle(FindOrganizationByTagIdQuery request, CancellationToken cancellationToken)
        {
            var TagUserList = await _unitOfWork.PeopleRepository.FindByTagIdAsync<Organization>(request.TagId);
            return _mapper.Map<List<OrganizationVm>>(TagUserList);
        }
    }
}
