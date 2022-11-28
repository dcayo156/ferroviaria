using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Relationship.Commands.CreateRelationship
{
    public class CreateRelationshipCommandHandler : IRequestHandler<CreateRelationshipCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateRelationshipCommandHandler> _logger;

        private async Task<bool> IsRelationshipsCorrespond(RelationshipCommand[] items)
        {
            var relationshipType1 = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(items[0].RelationshipTypeID);
            var relationshipType2 = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(items[1].RelationshipTypeID);
            return ((items[1].RelationshipTypeID == relationshipType1?.RelationshipTypeRequiredID) || relationshipType1?.RelationshipTypeRequiredID == null) &&
                   ((items[0].RelationshipTypeID == relationshipType2?.RelationshipTypeRequiredID || relationshipType2?.RelationshipTypeRequiredID == null));
        }
        public CreateRelationshipCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateRelationshipCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateRelationshipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var relationshipType = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(request.Relation!.RelationshipTypeID);
                if (relationshipType is null)
                    throw new Exception("Los RelationshipTypeID no tiene una relacion correspondiente");

                List<RelationshipCommand> relationshipCommands = new List<RelationshipCommand>();
                relationshipCommands.Add(request.Relation);
                relationshipCommands.Add(new RelationshipCommand()
                {
                    PersonID = request.PersonId,
                    RelationshipTypeID = relationshipType.RelationshipTypeRequiredID!.Value,
                    IsNeutral = request.Relation.IsNeutral
                });

                LaJuana.Domain.Relationship relationship = new();
                foreach (var item in relationshipCommands)
                    relationship.RelationshipDetails.Add(new RelationshipDetail()
                    {
                        PersonID = item.PersonID,
                        RelationshipTypeID = item.RelationshipTypeID,
                        IsNeutral = item.IsNeutral
                    });
                _unitOfWork.RelationshipRepository.AddEntity(relationship);
                var result = await _unitOfWork.Complete();
                _unitOfWork.RelationshipRepository.AddEntityLucene(relationshipCommands[0], relationshipCommands[1]);
                return relationship.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
