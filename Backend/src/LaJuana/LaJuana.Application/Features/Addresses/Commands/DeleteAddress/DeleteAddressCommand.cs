using MediatR;

namespace LaJuana.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest
    {
        public Guid Id { get; set; }       
    }
}
