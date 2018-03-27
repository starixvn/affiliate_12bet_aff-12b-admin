using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public AuthRepository()
        {
        }

        public AuthorizedUserInfo Auth(UserLogin userLogin)
        {
            const string spKey = "Aff_AffiliateLogin";

            using (var conn = new DataBox(spKey))
            {
                conn.DB.ExecuteNonQuery(parameterSet: userLogin);
                var result = new AuthorizedUserInfo
                {

                };
                return result;
            }

        }
    }
}
