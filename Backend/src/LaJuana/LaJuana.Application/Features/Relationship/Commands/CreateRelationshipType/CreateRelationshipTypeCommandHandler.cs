using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;


namespace LaJuana.Application.Features.RelationshipType.Commands.CreateRelationshipType
{
    public class CreateRelationshipTypeCommandHandler : IRequestHandler<CreateRelationshipTypeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateRelationshipTypeCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRelationshipTypeCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateRelationshipTypeCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateRelationshipTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.Items.Count() == 0 || request.Items.Count() >= 3)
                throw new ArgumentException();

            var relationshipEntity = _mapper.Map<LaJuana.Domain.RelationshipType>(request.Items.FirstOrDefault());
            await _unitOfWork.RelationshipTypeRepository.AddAsync(relationshipEntity);

            if (request.Items.Count() == 1)
            {
                var relationshipType = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(relationshipEntity.Id);
                relationshipType.RelationshipTypeRequiredID = relationshipType.Id;
                await _unitOfWork.Complete();
            }
            else
            {
                var relationshipType = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(relationshipEntity.Id);
                var secondItem = request.Items[1];
                secondItem.RelationshipTypeRequiredID = relationshipType!.Id;
                var secondRelationshipEntity = _mapper.Map<LaJuana.Domain.RelationshipType>(secondItem);
                await _unitOfWork.RelationshipTypeRepository.AddAsync(secondRelationshipEntity);
                relationshipType.RelationshipTypeRequiredID = secondRelationshipEntity.Id;
                await _unitOfWork.Complete();
            }
            return relationshipEntity.Id;
        }
    }
}
