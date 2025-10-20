
namespace Fanex.Affiliate.Admin.Core.Commands.Customer
{
    public class UpdateAffTypeCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliateAffType_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int AffType { get; set; }
    }
}
