using MediatR;

namespace LaJuana.Application.Features.Phones.Commands.CreatePhone
{
    public class CreatePhoneCommand : IRequest<Guid>
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneDescription { get; set; } = string.Empty;
        public Guid PersonId { get; set; }
    }
}
