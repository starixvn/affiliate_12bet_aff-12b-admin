using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public AuthRepository()
        {
        }

        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            const string spKey = "Aff_CheckIn";

            using (var dataBox = new DataBox(spKey))
            {
                dataBox.DB.ExecuteNonQuery(onUserIdParam);
            }

            return onUserIdParam.On_UserId;
        }

        public AuthorizedUserInfo GetUserInfo(UserLoginParam userLoginParam)
        {
            const string spKey = "Aff_AffiliateLogin_GetUserInfo";
            AuthorizedUserInfo result = null;

            using (var dataBox = new DataBox(spKey))
            {
                result = dataBox.DB.QueryEntity<AuthorizedUserInfo>(userLoginParam);
            }

            return result;
        }
    }
}
