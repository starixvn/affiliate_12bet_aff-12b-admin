namespace Fanex.Affiliate.User.Core.MetalModels.BetList
{
    public class BetListCashoutDetailParam
    {
        public long RefNo { get; set; }

        public decimal ExchangeRate { get; set; }

        public long TransId { get; set; }

        public long BetdaqId { get; set; }

        public int BetType { get; set; }

        public bool IsCashOut { get; set; }
    }
}
