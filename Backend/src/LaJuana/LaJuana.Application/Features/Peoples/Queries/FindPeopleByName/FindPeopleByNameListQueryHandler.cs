using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Application.Features.Peoples.Queries.GetPeoples;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Peoples.Commands.GetPeoples
{
    public class FindPeopleByNameListQueryHandler : IRequestHandler<FindPeopleByNameListQuery, List<PeopleVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public FindPeopleByNameListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_organizationRepository = organizationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PeopleVm>> Handle(FindPeopleByNameListQuery request, CancellationToken cancellationToken)
        {
            var userList = await _unitOfWork.PeopleRepository.FindUserByNameAsync(request.Name);

            return _mapper.Map<List<PeopleVm>>(userList);
        }
    }
}

