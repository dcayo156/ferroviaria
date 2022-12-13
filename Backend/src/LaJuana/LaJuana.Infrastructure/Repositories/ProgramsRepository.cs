using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Infrastructure.Repositories
{
    public class ProgramsRepository : RepositoryBase<Program>, IProgramsRepository
    {
        public ProgramsRepository(LaJuanaDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Program>> GetListPrograms()
        {
            return await _context.Programs!
                .OrderBy(p => p.Name)  
                .ToListAsync();

        }
        public async Task<Program> FindByIdAsync(Guid Id)
        {
            var program = await _context.Programs!.Where(p => p.Id == Id)
                .OrderBy(x => x.Name)            
                .FirstOrDefaultAsync();

            return program;
        }
    }
}
