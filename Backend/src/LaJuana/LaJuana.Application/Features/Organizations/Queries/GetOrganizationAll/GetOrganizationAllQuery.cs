using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Organizations.Queries.GetOrganizationAll
{
    public class GetOrganizationAllQuery : IRequest<List<OrganizationVm>>
    {
        public GetOrganizationAllQuery()
        {             
        }
    }
}
