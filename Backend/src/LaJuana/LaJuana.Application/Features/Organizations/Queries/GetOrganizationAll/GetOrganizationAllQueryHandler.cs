using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Organizations.Queries.GetOrganizationAll
{
    public class GetOrganizationAllQueryHandler : IRequestHandler<GetOrganizationAllQuery, List<OrganizationVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrganizationAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrganizationVm>> Handle(GetOrganizationAllQuery request, CancellationToken cancellationToken)
        {
            var organizationList = await _unitOfWork.OrganizationRepository.GetOrganizationAll();

            return _mapper.Map<List<OrganizationVm>>(organizationList);
        }
    }
}
