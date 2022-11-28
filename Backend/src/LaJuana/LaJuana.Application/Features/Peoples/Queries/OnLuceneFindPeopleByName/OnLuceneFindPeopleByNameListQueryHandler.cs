using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Lucene.FindUserByNameLucene
{
	public class OnLuceneFindPeopleByNameListQueryHandler : IRequestHandler<OnLuceneFindPeopleByNameQuery, List<PeopleVm>>
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OnLuceneFindPeopleByNameListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PeopleVm>> Handle(OnLuceneFindPeopleByNameQuery request, CancellationToken cancellationToken)
        {
            var userList = _unitOfWork.PeopleRepository.OnLuceneFindPeopleByName(request.Name);

            return _mapper.Map<List<PeopleVm>>(userList);
        }
    }
}

