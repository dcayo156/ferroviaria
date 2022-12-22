namespace LaJuana.Application.Contracts.Persistence
{
    public interface IHelpersDocument
    {
        void CheckDirectory(string path);
        Task SaveFile(string file, string filePath);
    }
}
