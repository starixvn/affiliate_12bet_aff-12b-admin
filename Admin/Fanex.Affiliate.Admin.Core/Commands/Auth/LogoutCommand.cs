using Fanex.Data;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Commands.Auth
{
    public class LogoutCommand : NonQueryCommand
    {
        private const string SP_NAME = "Aff_Logout";

        [SpParam(SqlParamName = "On_UserId")]
        public int OnUserId { get; set; }

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => OnUserId > 0;
    }
}
