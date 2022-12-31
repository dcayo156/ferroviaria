using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IDocumentsRepository : IAsyncRepository<Document>
    {
        Task<IEnumerable<Document>> GetListDocuments();
        Task<Document> FindByIdAsync(Guid Id);
        Task<int> GetCountDocuments();
    }
}
