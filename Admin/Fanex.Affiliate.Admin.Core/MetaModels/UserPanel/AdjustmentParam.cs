using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class AdjustmentParam
    {
        public int AffId { get; set; }

        public int UserId { get; set; }

        [RegularExpression(@"^(?!\.)\d*\.?\d+$")]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string Ip { get; set; }
    }
}
