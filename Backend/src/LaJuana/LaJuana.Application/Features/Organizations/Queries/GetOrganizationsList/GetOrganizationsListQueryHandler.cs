using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Organizations.Queries.GetOrganizationsList
{
    public class GetOrganizationsListQueryHandler : IRequestHandler<GetOrganizationsListQuery, List<OrganizationVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public GetOrganizationsListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_organizationRepository = organizationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrganizationVm>> Handle(GetOrganizationsListQuery request, CancellationToken cancellationToken)
        {
            var organizationList = await  _unitOfWork.OrganizationRepository.GetOrganizationByUsername(request.Username);

            return _mapper.Map<List<OrganizationVm>>(organizationList);
        }
    }
}
