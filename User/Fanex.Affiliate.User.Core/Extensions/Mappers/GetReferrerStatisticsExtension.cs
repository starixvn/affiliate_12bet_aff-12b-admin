namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetReferrerStatisticsExtension
    {
        public static void Map(this GetReferrerStatisticsCriteria criteria, ReferrerStatisticsParam param)
        {
            criteria.AffId = param.AffId;
            criteria.FromDate = param.FromDate;
            criteria.ToDate = param.ToDate;
            criteria.DeviceType = param.DeviceType;
            criteria.TotalClicks = param.TotalClicks;
            criteria.UniqueClicks = param.UniqueClicks;
            criteria.TotalSignUps = param.TotalSignUps;
            criteria.TotalFdms = param.TotalFdms;
            criteria.CountryCodes = param.CountryCodes;
            criteria.IsInternal = param.IsInternal;
        }
    }
}
