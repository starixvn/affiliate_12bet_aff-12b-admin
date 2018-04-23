using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            const string spKey = "Aff_CheckIn";
            var dbMode = onUserIdParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var dataBox = new ObjectDBBuilder(spKey, dbMode))
            {
                dataBox.DB.ExecuteNonQuery(onUserIdParam);
            }

            return onUserIdParam.On_UserId;
        }

        public AuthorizedUserInfo GetUserInfo(UserLoginParam userLoginParam)
        {
            const string spKey = "Aff_AffiliateLogin_UserInfoSel";
            var dbMode = userLoginParam.IsInternal ? DBMode.View : DBMode.Main;
            AuthorizedUserInfo result = null;

            using (var dataBox = new ObjectDBBuilder(spKey, dbMode))
            {
                result = dataBox.DB.QueryEntity<AuthorizedUserInfo>(userLoginParam);
            }

            return result;
        }

        public void UpdateLoginResult(LoginResultParam loginResultParam)
        {
            const string spKey = "Aff_AffiliateLogin_LoginResultUpd";
            var dbMode = loginResultParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var dataBox = new ObjectDBBuilder(spKey, dbMode))
            {
                dataBox.DB.ExecuteNonQuery(loginResultParam);
            }
        }

        public string GetSessionId(int onUserId)
        {
            const string spKey = "Aff_SessionIdSel";

            using (var dataBox = new ObjectDBBuilder(spKey, DBMode.View))
            {
                var objectDb = dataBox.DB;

                objectDb.AddInParameter("OnUserId", System.Data.SqlDbType.Int, onUserId);

                return objectDb.ExecuteScalar() as string;
            }

        }
    }
}
