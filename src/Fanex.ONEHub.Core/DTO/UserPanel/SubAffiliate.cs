namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class SubAffiliate
    {
        public int AffId { get; set; }


        public string UserName { get; set; }

        public string AffName => UserName;

        public decimal Commission { get; set; }

        public decimal SubAffCommission { get; set; }
    }
}
