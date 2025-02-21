using Fanex.Data;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Commands.Auth
{
    public class CheckUserPermissionCommand : NonQueryCommand
    {
        private const string SP_NAME = "Aff_Con_CheckPermission";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => UserId > 0;

        [SpParam(SqlParamName = "CustId")]
        public int UserId { get; set; }

        public string FuncName { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
