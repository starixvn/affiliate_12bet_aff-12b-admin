namespace Fanex.Affiliate.Link.Commands
{
    public class CheckMarketingLinkCommand : BaseMarketingLinkCommand
    {
        public bool IsExistedTracking { get; set; }

        public override string GetSettingKey() => "Aff_User_AffMediaLink_CheckExistedTracking";

        public override bool IsValid() => AffId > 0;
    }
}