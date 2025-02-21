namespace Fanex.Affiliate.User.Core.Commands
{
    using Data;
    using Data.Repository;
    using Utilities;

    public class CheckInCommand : NonQueryCommand
    {
        private const string SpKey = "Aff_CheckIn";

        public int CustId { get; set; }

        public string NetSessionId { get; set; }

        public int ServerId { get; set; }

        public string Ip { get; set; }

        public int SubAccId { get; set; }

        public int On_UserId { get; set; }

        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => CustId > 0;
    }
}