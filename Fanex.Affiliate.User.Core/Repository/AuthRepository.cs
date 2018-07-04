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

            using (var objectDb = new ObjectDBBuilder(spKey, dbMode))
            {
                objectDb.DB.ExecuteNonQuery(onUserIdParam);
            }

            return onUserIdParam.On_UserId;
        }

        public AuthorizedUserInfo GetUserInfo(UserLoginParam userLoginParam)
        {
            const string spKey = "Aff_User_Login_UserInfoSel";
            var dbMode = userLoginParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(spKey, dbMode))
            {
                return objectDb.DB.QueryEntity<AuthorizedUserInfo>(userLoginParam);
            }
        }

        public void UpdateLoginResult(LoginResultParam loginResultParam)
        {
            const string spKey = "Aff_User_Login_ResultUpd";
            var dbMode = loginResultParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(spKey, dbMode))
            {
                objectDb.DB.ExecuteNonQuery(loginResultParam);
            }
        }

        public string GetSessionId(int onUserId)
        {
            const string spKey = "Aff_SessionIdSel";

            using (var objectDb = new ObjectDBBuilder(spKey))
            {
                objectDb.DB.AddInParameter("OnUserId", System.Data.SqlDbType.Int, onUserId);

                return objectDb.DB.ExecuteScalar() as string;
            }

        }

        public bool Recheckin(int onUserId, bool isInternal)
        {
            const string spKey = "Aff_Recheckin";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var parameterSet = new
            {
                On_UserId = onUserId,
                IsKickedOut = true
            };

            using (var objectDb = new ObjectDBBuilder(spKey, dbMode))
            {
                objectDb.DB.ExecuteNonQuery(parameterSet);
            }

            return parameterSet.IsKickedOut;
        }
    }
}
