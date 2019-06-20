using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Customer
{
    public class UpdateAffTypeParam : AffiliateBasicInfoParam
    {
        public byte AffType { get; set; }
    }
}
