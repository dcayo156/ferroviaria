using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Infrastructure.Repositories
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        private const string _indexPeopleLucene = "people";
        public OrganizationRepository(LaJuanaDbContext context) : base(context)
        { 
        }
        public async Task<Organization> GetOrganizationByNombre(string nombreOrganization)
        {
            return await _context.Organizations!.Where(o => o.Name == nombreOrganization).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Organization>> GetOrganizationByUsername(string username)
        {
            return await _context.Organizations!.Where(v => v.CreatedBy == username).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindByTagIdAsync<T>(Guid tagId)
         where T : Person
        {
            var tags = await _context.Tags!.
            Where(p => p.Id.Equals(tagId))
            .Include(x => x.Persons)
            .FirstOrDefaultAsync();
            if (tags == null)
                throw new EntryPointNotFoundException();
            var organizations = tags?.Persons.Where(x => x is T).OfType<T>().ToArray();
            return organizations!;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationAll()
        {
            return await _context.Organizations!.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
