
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Queries.FindTagPeopleById
{
    public class FindTagPeopleByIdQuery : IRequest<List<TagVm>>
    {
        public Guid Id { get; set; }

        public FindTagPeopleByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
