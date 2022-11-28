using LaJuana.Application.Models.ViewModels;
using MediatR;
namespace LaJuana.Application.Features.Peoples.Queries.FindPeopleById
{
    public class FindPeopleByIdQuery : IRequest<PeopleFullVm>
    {
        public Guid Id { get; set; }

        public FindPeopleByIdQuery(Guid id)
        {
            Id = id;
        }
    } 
}
