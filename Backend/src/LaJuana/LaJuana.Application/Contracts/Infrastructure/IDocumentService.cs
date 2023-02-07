namespace LaJuana.Application.Contracts.Infrastructure
{
    public interface IDocumentService
    {
        Task<string> SaveIcon(string iconName, string file);
        Task<string> SaveDocument(string path, string documentName, string file, bool isDocument, bool isNew);
    }
}
