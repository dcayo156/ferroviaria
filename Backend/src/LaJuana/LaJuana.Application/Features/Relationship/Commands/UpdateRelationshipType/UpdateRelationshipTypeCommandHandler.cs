using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace LaJuana.Application.Features.RelationshipType.Commands.UpdateRelationshipType
{
    public class UpdateRelationshipTypeCommandHandler : IRequestHandler<UpdateRelationshipTypeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateRelationshipTypeCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateRelationshipTypeCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateRelationshipTypeCommandHandler> logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateRelationshipTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var relationshipType = await _unitOfWork.RelationshipTypeRepository.GetByIdAsync(request.Id);
                _mapper.Map(request, relationshipType, typeof(UpdateRelationshipTypeCommand), typeof(UpdateRelationshipTypeCommand));

                var relationshipTypeRep = _unitOfWork.RelationshipTypeRepository.UpdateAsync(relationshipType);
                var result = await _unitOfWork.Complete();
                return relationshipType.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
