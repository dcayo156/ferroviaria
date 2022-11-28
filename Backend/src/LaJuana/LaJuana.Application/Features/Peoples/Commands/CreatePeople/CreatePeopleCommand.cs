using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Commands
{
    public class CreatePeopleCommand : IRequest<Guid>
    {
        public PersonCommand? Person { get; set; }
        public List<Guid>? Tags { get; set; }
    }

    public class PersonCommand
    {
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public PronounPreference? PronounPreference { get; set; }
        public List<AddressVm>? Addresses { get; set; }
        public List<MailVm>? Mails { get; set; }
        public List<PhoneVm>? Phones { get; set; }
    }
}
