namespace Fanex.Affiliate.User.Core.Commands.Marketing
{
    public class ToggleMarketingLinkCommand : BaseNonQueryCommand
    {
        public int AffId { get; set; }

        public string ChannelCode { get; set; }

        public string LanguageCode { get; set; }

        public string AffMediaLink { get; set; }

        public bool IsOn { get; set; }

        public override string GetSettingKey() => "Aff_User_AffMediaLink_ToggleChannel";

        public override bool IsValid() => AffId > 0;
    }
}
