using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.Auth
{
    public class LoginCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Login";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string UserName { get; set; }

        [SpParam(SqlParamName = "UserPwd")]
        public string Password { get; set; }

        [SpParam(SqlParamName = "HostName")]
        public string UserHostName { get; set; }
    }
}
