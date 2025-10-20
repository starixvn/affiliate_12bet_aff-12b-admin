using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.UserAccount
{
    public class UpdateUserAccountCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliateInfo_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ContactApps { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [SpParam(SqlParamName = "Postal")]
        public string Postcode { get; set; }

        public string Country { get; set; }

        public bool MailOption { get; set; }

        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        [SpParam(SqlParamName = "NoSiteDesc")]
        public string NoSiteDesc { get; set; }

        public string PreferredPayment { get; set; }

        [SpParam(SqlParamName = "Player_Username")]
        public string PlayerUsername { get; set; }

        public string NetellerAccountId { get; set; }

        public string MoneybookersEmail { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        [SpParam(SqlParamName = "BankSwiftCode")]
        public string BankCode { get; set; }

        public string BankAccountName { get; set; }

        [SpParam(SqlParamName = "BankAccountNo")]
        public string BankAccountNumber { get; set; }

        public int? Player_CustId { get; set; }

        public int? Player_Currency { get; set; }
    }
}
