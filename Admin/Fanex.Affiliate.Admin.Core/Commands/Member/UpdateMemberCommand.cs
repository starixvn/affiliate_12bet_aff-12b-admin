using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.Member
{
    public class UpdateMemberCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Member_RemoveStatus_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => MemberId > 0;

        [SpParam(SqlParamName = "CustId")]
        public int MemberId { get; set; }

        public bool IsInclude { get; set; }

        public bool IsRemoved { get; set; }
    }
}
