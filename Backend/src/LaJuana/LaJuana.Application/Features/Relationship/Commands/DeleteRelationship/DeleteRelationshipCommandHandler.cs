using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Relationship.Commands.DeleteRelationship
{
    public class DeleteRelationshipCommandHandler : IRequestHandler<DeleteRelationshipCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteRelationshipCommandHandler> _logger;

        public DeleteRelationshipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteRelationshipCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteRelationshipCommand request, CancellationToken cancellationToken)
        {
            var relationships = await _unitOfWork.RelationshipRepository.GetAsync(x=>x.Id==request.Id,includeString: "RelationshipDetails");
            var relationshipToDelete = relationships.FirstOrDefault();

            if (relationshipToDelete == null)
            {
                _logger.LogError($"{request.Id} relationship no existe en el sistema");
                throw new NotFoundException(nameof(Relationship), request.Id);      
            }

            var relationshipDetails = relationshipToDelete.RelationshipDetails;
            foreach (var relationshipDetail in relationshipDetails)
            {
                _unitOfWork.Repository<RelationshipDetail>().DeleteEntity(relationshipDetail);
            }

            _unitOfWork.RelationshipRepository.DeleteEntity(relationshipToDelete);

            await _unitOfWork.Complete();
           
            _logger.LogInformation($"El {request.Id} relationship fue eliminado con exito");

            return Unit.Value;
        }
    }
}
