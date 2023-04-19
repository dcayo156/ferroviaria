using LaJuana.Application.Contracts.Identity;
using Microsoft.Extensions.Logging;
using System.DirectoryServices.AccountManagement;

namespace LaJuana.Identity.Services
{
    public class AuthWindowsServerService: IAuthWindowsServerService
    {
        private readonly ILogger<AuthWindowsServerService> _logger;
        public AuthWindowsServerService(ILogger<AuthWindowsServerService> logger) { _logger = logger; }
        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            bool result = false;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "fosa.com", "fosa\\{ldapadmin}", "123ppp+++"))
            {
                result = context.ValidateCredentials(username, pwd);
            }
            return result;           
        }
        //public bool IsAuthenticated2(string serverName, string domain, string username, string pwd)
        //{
        //    //var ldap = GetCurrentDomainPath();            
        //    var domainArray = domain.Split('.');
        //    string domainAndUsername = domainArray[0] + @"\" + username;
        //    DirectoryEntry entry = new DirectoryEntry($"LDAP://{serverName}/DC={domainArray[0]},DC={domainArray[1]}", domainAndUsername, pwd);
        //    try
        //    {
        //        DirectorySearcher search = new DirectorySearcher(entry);

        //        search.Filter = "(SAMAccountName=" + username + ")";
        //        search.PropertiesToLoad.Add("cn");
        //        search.PropertiesToLoad.Add("OU");
        //        SearchResult result = search.FindOne();

        //        if (null == result)
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error" + ex.Message);
        //    }

        //    return true;
        //}

        //public bool IsAuthenticated3(string domain, string username, string pwd)
        //{
        //    bool result = false;
        //    using (PrincipalContext context = new PrincipalContext(ContextType.Domain, domain))
        //    {
        //        result = context.ValidateCredentials(username, pwd);
        //    }
        //    return result;
        //}
        //private string GetCurrentDomainPath()
        //{
        //    DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

        //    return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        //}
    }
}
