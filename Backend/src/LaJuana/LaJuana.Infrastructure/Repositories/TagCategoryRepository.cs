using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
namespace LaJuana.Infrastructure.Repositories
{
    public class TagCategoryRepository : RepositoryBase<TagCategory>, ITagCategoryRepository
    {
        private const string _indexTagLucene = "tagCategory";
        public TagCategoryRepository(LaJuanaDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<TagCategory>> GetTagCategoryListAsync()
        {
            return await _context.TagCategories!
                .OrderBy(p => p.Description)
                .Include(x => x.Tags.OrderBy(t => t.Name))
                .ToListAsync();
        }
    }
}

