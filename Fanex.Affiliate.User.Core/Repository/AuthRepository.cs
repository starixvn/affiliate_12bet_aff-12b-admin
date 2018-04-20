using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            const string spKey = "Aff_CheckIn";

            using (var dataBox = new ObjectDBBuilder(spKey))
            {
                dataBox.DB.ExecuteNonQuery(onUserIdParam);
            }

            return onUserIdParam.On_UserId;
        }

        public AuthorizedUserInfo GetUserInfo(UserLoginParam userLoginParam)
        {
            const string spKey = "Aff_AffiliateLogin_GetUserInfo";
            AuthorizedUserInfo result = null;

            using (var dataBox = new ObjectDBBuilder(spKey))
            {
                result = dataBox.DB.QueryEntity<AuthorizedUserInfo>(userLoginParam);
            }

            return result;
        }

        public void UpdateLoginResult(LoginResultParam loginResultParam)
        {
            const string spKey = "Aff_AffiliateLogin_LoginResultUpd";

            using (var dataBox = new ObjectDBBuilder(spKey))
            {
                dataBox.DB.ExecuteNonQuery(loginResultParam);
            }
        }

        public string GetSessionId(int onUserId)
        {
            const string spKey = "Aff_SessionIdSel";

            using (var dataBox = new ObjectDBBuilder(spKey))
            {
                var objectDb = dataBox.DB;

                objectDb.AddInParameter("OnUserId", System.Data.SqlDbType.Int, onUserId);

                return objectDb.ExecuteScalar() as string;
            }

        }
    }
}
