namespace LaJuana.Application.Contracts.Identity
{
    public interface IAuthWindowsServerService
    {
        bool IsAuthenticated(string domain, string username, string pwd);       
    }
}
