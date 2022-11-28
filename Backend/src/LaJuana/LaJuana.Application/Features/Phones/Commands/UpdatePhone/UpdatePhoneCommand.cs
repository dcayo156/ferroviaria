using MediatR;

namespace LaJuana.Application.Features.Phones.Commands.UpdatePhone
{
    public class UpdatePhoneCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneDescription { get; set; } = string.Empty;
        public Guid PersonId { get; set; }

    }
}
