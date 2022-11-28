using System;
using System.Linq.Expressions;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;

namespace LaJuana.Application.Features.Relationship.Queries.GetRelationShipsByPersonId
{
	public class GetRelationShipsByPersonIdQueryHandler : IRequestHandler<GetRelationShipsByPersonIdQuery, List<RelationShipByPersonVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRelationShipsByPersonIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RelationShipByPersonVm>> Handle(GetRelationShipsByPersonIdQuery request, CancellationToken cancellationToken)
        {
                var relantionShipList = await _unitOfWork.RelationshipDetailRepository.GetRelationShipsByPersonId(request.PersonId);
                return _mapper.Map<List<RelationShipByPersonVm>>(relantionShipList);

        }
    }
}

