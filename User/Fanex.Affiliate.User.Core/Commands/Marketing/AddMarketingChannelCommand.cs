namespace Fanex.Affiliate.User.Core.Commands.Marketing
{
    public class AddMarketingChannelCommand : BaseNonQueryCommand
    {
        public AddMarketingChannelCommand(int userId, string channelCode, string channelName)
        {
            AffId = userId;
            ChannelCode = channelCode;
            ChannelName = channelName;
        }

        public int AffId { get; set; }

        public string ChannelCode { get; set; }

        public string ChannelName { get; set; }

        public override string GetSettingKey() => "Aff_User_AffMediaChannel_Ins";

        public override bool IsValid() => AffId > 0;
    }
}
