using MediatR;

namespace LaJuana.Application.Features.Mails.Commands.DeleteMail
{
    public class DeleteMailCommand : IRequest
    {
        public Guid Id { get; set; }       
    }
}
