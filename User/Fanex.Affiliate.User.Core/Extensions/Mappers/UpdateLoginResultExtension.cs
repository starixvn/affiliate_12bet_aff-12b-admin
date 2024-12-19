namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Commands;
    using MetalModels;

    public static class UpdateLoginResultExtension
    {
        public static void Map(this UpdateLoginResultCommand command, LoginResultParam param)
        {
            command.AffId = param.AffId;
            command.IpAddress = param.IpAddress;
            command.DeviceInfo = param.DeviceInfo;
            command.Language = param.Language;
            command.IsSuccessful = param.IsSuccessful;
        }
    }
}
