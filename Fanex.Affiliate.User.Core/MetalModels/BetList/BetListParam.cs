using System;

namespace Fanex.Affiliate.User.Core.MetalModels.BetList
{
    public class BetListParam : BaseParam
    {
        public int AffId { get; set; }

        public string UserName { get; set; }

        public int SportType { get; set; }

        public DateTime Date { get; set; }
    }
}
