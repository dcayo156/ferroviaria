using LaJuana.Application.Contracts.Identity;
using System.DirectoryServices;
namespace LaJuana.Identity.Services
{
    public class AuthWindowsServerService: IAuthWindowsServerService
    { 
        public bool IsAuthenticated(string domain, string username, string pwd)
        {

            string domainAndUsername = domain + @"\" + username;
            //DirectoryEntry entry = new DirectoryEntry("", domainAndUsername, pwd);
            DirectoryEntry entry = new DirectoryEntry("LDAP://gmocsrpdc1/DC=gmsantacruz,DC=gov,DC=bo", domainAndUsername, pwd);

            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("OU");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
