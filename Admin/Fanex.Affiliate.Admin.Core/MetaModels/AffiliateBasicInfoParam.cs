using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels
{
    public class AffiliateBasicInfoParam
    {
        [Range(1, int.MaxValue)]
        public int AffId { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }
    }
}
