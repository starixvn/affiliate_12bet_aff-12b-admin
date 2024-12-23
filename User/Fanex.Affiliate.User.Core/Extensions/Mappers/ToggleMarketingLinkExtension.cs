namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Commands.Marketing;
    using MetalModels;

    public static class ToggleMarketingLinkExtension
    {
        public static void Map(this ToggleMarketingLinkCommand command, MarketingLinkParam param)
        {
            command.AffId = param.AffId;
            command.ChannelCode = param.ChannelCode;
            command.LanguageCode = param.LanguageCode;
            command.AffMediaLink = param.AffMediaLink;
            command.IsOn = param.IsOn;
        }
    }
}
