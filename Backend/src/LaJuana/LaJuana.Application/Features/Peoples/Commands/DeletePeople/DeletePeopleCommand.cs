using MediatR;

namespace LaJuana.Application.Features.Peoples.Commands.DeletePeople
{
    public class DeletePeopleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
