using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO.Auth;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public AuthRepository()
        {
        }

        public AuthorizedUserInfo Auth(string userName, string userPass, string loginIp, int serverId)
        {
            throw new NotImplementedException();
        }
    }
}
