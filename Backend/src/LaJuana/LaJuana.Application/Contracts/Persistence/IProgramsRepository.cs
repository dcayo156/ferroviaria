using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IProgramsRepository : IAsyncRepository<Program>
    {
        Task<IEnumerable<Program>> GetListPrograms();
        Task<Program> FindByIdAsync(Guid Id);
    }
}
