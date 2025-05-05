namespace Fanex.Affiliate.User.Core.Commands.Account
{
    public class CreateAffiliateCommand : BaseNonQueryCommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; } = string.Empty;

        public string Postal { get; set; }

        public string Country { get; set; }

        public string SiteUrl { get; set; } = string.Empty;

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        public string TargetCountryCode { get; set; }

        public string PreferredLanguage { get; set; }

        public static string PreferredPayment => "Player";

        public string Player_UserName { get; set; }

        public byte QuestionId { get; set; }

        public string Answer { get; set; }

        public string ContactApps { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string UserName { get; set; }

        public string AffType { get; set; }

        public string AffParentCode { get; set; }

        public int Player_CustId { get; set; }

        public int Player_Currency { get; set; }

        public override string GetSettingKey() => "Aff_User_Affiliate_Ins";

        public override bool IsValid() => true;
    }
}
