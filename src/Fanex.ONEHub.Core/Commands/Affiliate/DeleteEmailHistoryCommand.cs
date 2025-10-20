using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.Affiliate
{
    public class DeleteEmailHistoryCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_EmailHistory_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(EmailIdList);

        [SpParam(SqlParamName = "MIDs")]
        public string EmailIdList { get; set; }
    }
}
