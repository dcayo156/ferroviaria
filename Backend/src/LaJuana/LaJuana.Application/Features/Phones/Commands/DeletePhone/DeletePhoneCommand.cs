using MediatR;

namespace LaJuana.Application.Features.Phones.Commands.DeletePhone
{
    public class DeletePhoneCommand : IRequest
    {
        public Guid Id { get; set; }       
    }
}
