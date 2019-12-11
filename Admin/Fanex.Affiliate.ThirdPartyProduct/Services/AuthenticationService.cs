using System.Configuration;
using System.Linq;

namespace Fanex.Affiliate.ThirdPartyProduct.Site.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        public bool Auth(string userName, string pass)
        {
            var users = ConfigurationManager.AppSettings["AllowedUser"]?.Split(',');
            if (users.Any() && users.Contains(userName) && !string.IsNullOrEmpty(pass))
            {
                return pass.Equals(ConfigurationManager.AppSettings["SecretKey"]);
            }

            return false;
        }
    }
}