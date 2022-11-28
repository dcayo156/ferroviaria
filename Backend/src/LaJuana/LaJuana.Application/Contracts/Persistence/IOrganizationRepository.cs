using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IOrganizationRepository : IAsyncRepository<Organization>
    {
        Task<Organization> GetOrganizationByNombre(string nombreOrganization);
        Task<IEnumerable<Organization>> GetOrganizationByUsername(string username);
        Task<IEnumerable<T>> FindByTagIdAsync<T>(Guid tagId) where T : Person;
        Task<IEnumerable<Organization>> GetOrganizationAll();
    }
}
