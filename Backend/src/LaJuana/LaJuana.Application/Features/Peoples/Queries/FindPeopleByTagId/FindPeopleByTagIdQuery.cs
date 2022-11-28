using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Queries.FindPeopleByTagId
{
    public class FindPeopleByTagIdQuery : IRequest<List<PeopleVm>>
    {
        public Guid TagId { get; set; }
        public FindPeopleByTagIdQuery(Guid TagId)
        {
            this.TagId = TagId;
        }

    }
}
