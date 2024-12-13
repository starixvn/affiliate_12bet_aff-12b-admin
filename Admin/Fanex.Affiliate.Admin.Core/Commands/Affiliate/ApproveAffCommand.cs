
namespace Fanex.Affiliate.Admin.Core.Commands.Affiliate
{
    public class ApproveAffCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_Affiliate_Approved";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int AffId { get; set; }

        public string Password { get; set; }

        public string Status { get; set; }

        public string AffCode { get; set; }
    }
}
