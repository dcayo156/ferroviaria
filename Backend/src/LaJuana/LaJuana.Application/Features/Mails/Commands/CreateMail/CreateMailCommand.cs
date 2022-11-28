using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;

namespace LaJuana.Application.Features.Mails.Commands.CreateMail
{
    public class CreateMailCommand : IRequest<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string EmailDescription { get; set; } = string.Empty;
        public Guid PersonId { get; set; }
    }
}
