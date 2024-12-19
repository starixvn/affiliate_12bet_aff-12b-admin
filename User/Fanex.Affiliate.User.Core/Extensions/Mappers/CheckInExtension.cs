namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Commands;
    using MetalModels;

    public static class CheckInExtension
    {
        public static void Map(this CheckInCommand command, OnUserIdParam param)
        {
            command.CustId = param.CustId;
            command.NetSessionId = param.NetSessionId;
            command.ServerId = param.ServerId;
            command.Ip = param.Ip;
            command.SubAccId = param.SubAccId;
            command.On_UserId = param.On_UserId;
            command.IsInternal = param.IsInternal;
        }
    }
}
