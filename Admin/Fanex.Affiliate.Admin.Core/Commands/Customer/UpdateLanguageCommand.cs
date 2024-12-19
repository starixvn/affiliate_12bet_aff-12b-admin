namespace Fanex.Affiliate.Admin.Core.Commands.Customer
{
    public class UpdateLanguageCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliatePreferredLanguage_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public string PreferredLanguage { get; set; }
    }
}
