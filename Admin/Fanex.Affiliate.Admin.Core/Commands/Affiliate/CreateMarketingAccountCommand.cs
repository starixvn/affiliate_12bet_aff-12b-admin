namespace Fanex.Affiliate.Admin.Core.Commands.Affiliate
{
    public class CreateMarketingAccountCommand : BaseNonQueryCommand
    {
        public int AffType { get; set; }

        public string Password { get; set; } = string.Empty;

        public int GroupId {get;set;}

        // Contact Info
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool? Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ContactApps { get; set; }

        // Mailing Info
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postal { get; set; }

        public string Country { get; set; }

        // Website Info
        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        // Preference
        public string PreferredLanguage { get; set; }

        public int QuestionId { get; set; }

        public string Answer { get; set; }

        // Payment Info
        public string PreferredPayment { get; set; }

        public int? Player_CustId { get; set; }

        public string Player_UserName { get; set; }

        public int? Player_Currency { get; set; }

        // Others
        public string Language { get; set; }

        public string TargetCountryCode { get; set; }

        // Output
        public string Username { get; set; }

        public override string GetSettingKey()
        {
            return "Aff_Admin_MarketingAccount_Affiliate_Ins";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
