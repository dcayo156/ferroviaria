using LaJuana.Application.Contracts.Persistence;
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
            return await _context.InspectionTrains!.Where(x => x.Status == (int)DocumentStatus.Habilitado)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

        }
        public async Task<InspectionTrain> FindByIdAsync(Guid Id)
        {
            var InspectionTrains = await _context.InspectionTrains!.Where(p => p.Id == Id && p.Status == (int)DocumentStatus.Habilitado)
                .FirstOrDefaultAsync();
            return InspectionTrains;
        }
    }
}
