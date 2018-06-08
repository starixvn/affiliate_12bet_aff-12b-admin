using System;

namespace Fanex.Affiliate.User.Core.MetalModels.BetList
{
    public class BetListResultParam
    {
        public string UserName { get; set; }

        public int BetType { get; set; }

        public int MatchId { get; set; }

        public long? RefNo { get; set; }

        public DateTime? WinLossDate { get; set; }

        public int? RaceNo { get; set; }

        public int SportType { get; set; }

        public int? IsOutRight { get; set; }

        public string BetId { get; set; }

        public int Handicap2 { get; set; }
    }
}
