using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class PaymentUpdateParam : UserInfoParam
    {
        [Range(Validation.MinId, long.MaxValue)]
        public long TransId { get; set; }

        public string Status { get; set; }

        public string AdminDesc { get; set; }

        public string AffDesc { get; set; }

        [SpParam(Ignored = true)]
        public bool IsApproved { get; set; }
    }
}
