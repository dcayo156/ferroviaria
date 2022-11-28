using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;
using System.Linq.Expressions;

namespace LaJuana.Application.Features.Relationship.Queries.GetPersonByRelationshipType
{
    public class GetPersonByRelationshipTypeQueryHandler : IRequestHandler<GetPersonByRelationshipTypeQuery, List<PersonByRelationshipTypeVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByRelationshipTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<PersonByRelationshipTypeVm>> Handle(GetPersonByRelationshipTypeQuery request, CancellationToken cancellationToken)
        {
            if(request.FromLucene==true){
                
                var relationshipType1 = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(request.RelationshipTypeId);
                string descriptionRol="";
                switch(request.Gender){
                    case Gender.Personalizado: descriptionRol=relationshipType1!.NeutralDescription; break;
                    case Gender.Masculino: descriptionRol=relationshipType1!.MaleDescription; break;
                    case Gender.Femenino: descriptionRol=relationshipType1!.FemaleDescription; break;
                }
                var relantionShipList = await _unitOfWork.RelationshipRepository.GetPersonByRelationshipTypeFromLucene(descriptionRol,request.RelationshipTypeId);
                return _mapper.Map<List<PersonByRelationshipTypeVm>>(relantionShipList);
            }else{
                var relantionShipList = await _unitOfWork.RelationshipDetailRepository.GetPersonByRelationshipType(request.RelationshipTypeId,request.Gender);
                return _mapper.Map<List<PersonByRelationshipTypeVm>>(relantionShipList);
            }
            
            
        }
    }
}
