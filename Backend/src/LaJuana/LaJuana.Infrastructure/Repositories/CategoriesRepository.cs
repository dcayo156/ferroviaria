using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Infrastructure.Repositories
{
    public class CategoriesRepository : RepositoryBase<Category>, ICategoriesRepository
    {
        public CategoriesRepository(LaJuanaDbContext context) : base(context)
        {

        }
        public async Task<List<Category>> GetListCategories()
        {
            return await _context.Categories
                .Include(c=> c.ParentCategory)
                .OrderBy(p => p.Name)  
                .ToListAsync();

        }
        public async Task<List<Category>> FindByIdSubCategoryAsync(Guid Id)
        {
            return await _context.Categories!.Where(c => c.ParentCategoryId == Id)
                .OrderBy(p => p.Name)
                .ToListAsync();

        }
        public async Task<Category> FindByIdAsync(Guid Id)
        {
            var category = await _context.Categories!.Where(p => p.Id == Id)
                .Include(c => c.ParentCategory)
                .OrderBy(x => x.Name)            
                .FirstOrDefaultAsync();

            return category;
        }
    }
}
