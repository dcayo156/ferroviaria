using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;

namespace LaJuana.Application.Features.Organizations.Commands.CreateOrganization
{
    public class CreateOrganizationCommand : IRequest<Guid>
    {
        public PersonOrganizationCommand? Person { get; set; }
        public List<Guid>? Tags { get; set; }
    }

    public class PersonOrganizationCommand
    {
        public string Name { get; set; } = string.Empty;
        public List<AddressVm>? Addresses { get; set; }
        public List<MailVm>? Mails { get; set; }
        public List<PhoneVm>? Phones { get; set; }
    }
}
