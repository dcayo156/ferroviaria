using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Organizations.Queries.FindOrganizationByTagId
{
    public class FindOrganizationByTagIdQuery : IRequest<List<OrganizationVm>>
    {
        public Guid TagId { get; set; }

        public FindOrganizationByTagIdQuery(Guid TagId)
        {
            this.TagId = TagId;
        }

    }
}
