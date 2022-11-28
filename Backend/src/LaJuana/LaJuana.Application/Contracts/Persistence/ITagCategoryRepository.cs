using LaJuana.Domain;
using System;
using LaJuana.Application.Models.ViewModels;

namespace LaJuana.Application.Contracts.Persistence
{
	public interface ITagCategoryRepository : IAsyncRepository<TagCategory>
	{
		Task<IEnumerable<TagCategory>> GetTagCategoryListAsync();
	}
}

