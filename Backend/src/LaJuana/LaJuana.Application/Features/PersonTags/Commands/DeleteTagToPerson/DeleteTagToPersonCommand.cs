using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.PersonTags.Commands
{
    public class DeleteTagToPersonCommand : IRequest<Guid>
    {
        public Guid PersonId { get; set; }
        public Guid TagId { get; set; }
    }
}
