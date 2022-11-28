using MediatR;

namespace LaJuana.Application.Features.Relationship.Commands.DeleteRelationshipType
{
    public class DeleteRelationshipTypeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
