using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Infrastructure.Repositories
{
    public class DocumentsRepository : RepositoryBase<Document>, IDocumentsRepository
    {
        public DocumentsRepository(LaJuanaDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Document>> GetListDocuments()
        {
            return await _context.Documents!.Where(x => x.Status == (int)DocumentStatus.Habilitado)
                .OrderBy(p => p.Category.Name)
                .ToListAsync();

        }
        public async Task<Document> FindByIdAsync(Guid Id)
        {
            var Document = await _context.Documents!.Where(p => p.Id == Id && p.Status == (int)DocumentStatus.Habilitado)
                .FirstOrDefaultAsync();
            return Document;
        }
        public async Task<int> GetCountDocuments()
        {
            return await _context.Documents!.CountAsync();

        }
    }
}
