using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Features.Peoples.Queries.FindPeopleById;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Peoples.Queries.FindPeopleByNationalId
{
    public class FindPeopleByNationalIdQueryHandler : IRequestHandler<FindPeopleByNationalIdQuery, PeopleFullVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public FindPeopleByNationalIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PeopleFullVm> Handle(FindPeopleByNationalIdQuery request, CancellationToken cancellationToken)
        {

            var userList = await _unitOfWork.PeopleRepository.FindByNationalIdAsync(request.NationalId);

            return _mapper.Map<PeopleFullVm>(userList);
            
        }
    }
}
