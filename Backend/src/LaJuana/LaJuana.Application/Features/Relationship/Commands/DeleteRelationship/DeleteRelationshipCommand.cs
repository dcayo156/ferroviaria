using MediatR;

namespace LaJuana.Application.Features.Relationship.Commands.DeleteRelationship
{
    public class DeleteRelationshipCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
