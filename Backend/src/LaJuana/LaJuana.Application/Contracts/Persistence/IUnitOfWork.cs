using LaJuana.Domain.Common;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IProgramsRepository ProgramRepository { get; }
        ICategoriesRepository CategoryRepository { get; }
        IDocumentsRepository DocumentRepository { get; }
        IInspectionTrainsRepository InspectionTrainsRepository { get; }
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
    }
}
