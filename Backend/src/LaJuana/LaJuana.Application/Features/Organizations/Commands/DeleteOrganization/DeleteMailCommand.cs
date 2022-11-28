using MediatR;

namespace LaJuana.Application.Features.Organizations.Commands.DeleteOrganization
{
    public class DeleteOrganizationCommand : IRequest
    {
        public Guid Id { get; set; }       
    }
}
