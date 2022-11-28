using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Relationship.Queries.GetRelationShipTypes
{
    public class GetRelationShipTypesQueryHandler : IRequestHandler<GetRelationShipTypesQuery, List<RelationshipTypesVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRelationShipTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RelationshipTypesVM>> Handle(GetRelationShipTypesQuery request, CancellationToken cancellationToken)
        {
            var relantionShipList = await _unitOfWork.RelationshipTypeRepository.GetAllAsync();
            return _mapper.Map<List<RelationshipTypesVM>>(relantionShipList);
        }
    }
}
