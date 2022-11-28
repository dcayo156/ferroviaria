using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Peoples.Queries.GetListPeople
{
    public class GetListPeopleQueryHandler : IRequestHandler<GetListPeopleQuery, List<PeopleFullVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public GetListPeopleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_organizationRepository = organizationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PeopleFullVm>> Handle(GetListPeopleQuery request, CancellationToken cancellationToken)
        {
            var userList = await _unitOfWork.PeopleRepository.GetListPeople();

            return _mapper.Map<List<PeopleFullVm>>(userList);
        }
    }
}

