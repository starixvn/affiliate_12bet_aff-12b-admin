using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public class InsertExistedAccountCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Con_AdminInsFromExisted";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string UserName { get; set; }

        public int GroupId { get; set; }

        public int Creator { get; set; }

        [SpParam(Ignored = true)]
        public new int UserId { get; set; }
    }
}
