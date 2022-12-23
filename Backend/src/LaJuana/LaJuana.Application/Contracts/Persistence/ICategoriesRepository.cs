using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface ICategoriesRepository : IAsyncRepository<Category>
    {
        Task<IEnumerable<Category>> GetListCategories();
        Task<Category> FindByIdAsync(Guid Id);
    }
}
