using MediatR;

namespace LaJuana.Application.Features.Mails.Commands.UpdateMail
{
    public class UpdateMailCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string EmailDescription { get; set; } = string.Empty;
        public Guid PersonId { get; set; }

    }
}
