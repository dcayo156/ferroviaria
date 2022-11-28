using MediatR;

namespace LaJuana.Application.Features.Organizations.Commands.UpdateOrganization
{
    public class UpdateOrganizationCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
