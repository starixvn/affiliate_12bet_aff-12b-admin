namespace Fanex.Affiliate.User.Core.Commands
{
    using Data.Repository;
    using Data;
    using Utilities;

    public class RecheckinCommand : NonQueryCommand
    {
        private const string SpKey = "Aff_Recheckin";

        public RecheckinCommand(int onUserId, bool isInternal)
        {
            On_UserId = onUserId;
            IsInternal = isInternal;
        }

        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }

        public int On_UserId { get; set; }

        public bool IsKickedOut { get; set; } = true;

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => On_UserId > 0;
    }
}
