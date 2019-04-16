namespace Fanex.Affiliate.Admin.Core.MetaModels.Customer
{
    public class UpdateStatusParam : AffiliateBasicInfoParam
    {
        public int GroupId { get; set; } = -1;

        public string Status { get; set; }
    }
}
