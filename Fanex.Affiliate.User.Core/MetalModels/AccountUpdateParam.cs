namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class AccountUpdateParam
    {
        public int AffId { get; set; }

        public int UserId => AffId;

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        public string Player_UserName { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
