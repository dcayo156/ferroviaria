using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Infrastructure.Repositories
{
    public class InspectionTrainsRepository : RepositoryBase<InspectionTrain>, IInspectionTrainsRepository
    {
        public InspectionTrainsRepository(LaJuanaDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<InspectionTrain>> GetListInspectionTrains()
        {
            return await _context.InspectionTrains!
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

        }
        public async Task<InspectionTrain> FindByIdAsync(string Code)
        {
            var InspectionTrains = await _context.InspectionTrains!.Where(p => p.Codigo == Code)
                .FirstOrDefaultAsync();
            return InspectionTrains;
        }
    }
}
