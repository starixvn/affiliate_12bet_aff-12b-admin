namespace Fanex.Affiliate.User.Core.Commands
{
    public class UpdateAccountInfoCommand : BaseNonQueryCommand
    {
        public int AffId { get; set; }

        public int UserId => AffId;

        public string Phone { get; set; }

        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ContactApps { get; set; }

        public int DefaultBaseCurrency { get; set; }

        public override string GetSettingKey() => "Aff_User_UserInfoUpd";

        public override bool IsValid() => AffId > 0;
    }
}
