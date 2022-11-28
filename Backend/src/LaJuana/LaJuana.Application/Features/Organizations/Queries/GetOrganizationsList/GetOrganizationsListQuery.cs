using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Organizations.Queries.GetOrganizationsList
{
    public class GetOrganizationsListQuery : IRequest<List<OrganizationVm>>
    {
        public string Username { get; set; } = String.Empty;

        public GetOrganizationsListQuery(string username)
        { 
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
