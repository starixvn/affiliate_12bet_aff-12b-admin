namespace Fanex.Affiliate.Admin.Core.MetaModels.Affiliate
{
    public class ApproveAffiliateParam
    {
        public int UserId { get; set; }

        public int AffId { get; set; }

        public string Password { get; set; }

        public string Status { get; set; }

        public string Ip { get; set; }
    }
}
