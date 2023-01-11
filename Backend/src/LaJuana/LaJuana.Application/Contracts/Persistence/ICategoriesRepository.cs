using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface ICategoriesRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetListCategories();
        Task<Category> FindByIdAsync(Guid Id);
        Task<List<Category>> FindByIdSubCategoryAsync(Guid Id);
    }
}
