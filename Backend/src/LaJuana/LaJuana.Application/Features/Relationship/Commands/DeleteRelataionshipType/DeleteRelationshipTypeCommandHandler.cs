using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Relationship.Commands.DeleteRelationshipType
{
    public class DeleteRelationshipTypeCommandHandler : IRequestHandler<DeleteRelationshipTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteRelationshipTypeCommandHandler> _logger;

        public DeleteRelationshipTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteRelationshipTypeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteRelationshipTypeCommand request, CancellationToken cancellationToken)
        {
            var relationshipType1 = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(request.Id); 
            var relationshipType2 = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(relationshipType1!.RelationshipTypeRequiredID!.Value);

            relationshipType1.RelationshipTypeRequiredID = null;
            relationshipType2.RelationshipTypeRequiredID = null;

            await _unitOfWork.Complete();

            _unitOfWork.RelationshipTypeRepository.DeleteEntity(relationshipType1!);
            _unitOfWork.RelationshipTypeRepository.DeleteEntity(relationshipType2!);

            await _unitOfWork.Complete();

            return Unit.Value;
        }
    }
}
