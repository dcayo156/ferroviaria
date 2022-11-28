using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Relationship.Queries.GetRelationShipGroupTypes
{
    public class GetRelationShipGroupTypesQueryHandler : IRequestHandler<GetRelationShipGroupTypesQuery, List<RelationshipGroupTypesVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRelationShipGroupTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RelationshipGroupTypesVM>> Handle(GetRelationShipGroupTypesQuery request, CancellationToken cancellationToken)
        {
            var relantionShipList = await _unitOfWork.RelationshipTypeRepository.GetAllAsync();
            if (relantionShipList == null)
            {
                throw new Exception("No se encontro registros");
            }
            var relationshipTypesGroupVM = new List<RelationshipGroupTypesVM>();
            foreach (var relantionShipitem in relantionShipList)
            {
                if (relationshipTypesGroupVM.Count(r => r.IdRelationType1.Equals(relantionShipitem.RelationshipTypeRequiredID)) == 0)
                {
                    var item = new RelationshipGroupTypesVM();
                    item.Id = relantionShipitem.Id;
                    item.IdRelationType1 = relantionShipitem.Id;
                    item.IdRelationType2 = relantionShipitem.RelationshipTypeRequiredID;
                    item.RelationShipName1 = $"{relantionShipitem.MaleDescription} / {relantionShipitem.FemaleDescription} / {relantionShipitem.NeutralDescription}";
                    item.RelationShipName2 = relantionShipList.Where(re => re.Id.Equals(relantionShipitem.RelationshipTypeRequiredID))
                        .Select(re => new RelationshipTypesVM()
                        {
                            FemaleDescription = re.FemaleDescription,
                            MaleDescription = re.MaleDescription,
                            NeutralDescription = re.NeutralDescription,
                        }).FirstOrDefault()!.RelationshipName;
                    relationshipTypesGroupVM.Add(item);
                }
            }
            return relationshipTypesGroupVM;
        }
    }
}
