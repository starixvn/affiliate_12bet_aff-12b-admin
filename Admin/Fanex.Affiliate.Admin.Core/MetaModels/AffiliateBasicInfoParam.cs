using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels
{
    public class AffiliateBasicInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int AffId { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }
    }
}
