namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetLandingPageStatisticsExtension
    {
        public static void Map(this GetLandingPageStatisticsCriteria criteria, LandingPageStatisticsParam param)
        {
            criteria.AffId = param.AffId;
            criteria.FromDate = param.FromDate;
            criteria.ToDate = param.ToDate;
            criteria.DeviceType = param.DeviceType;
            criteria.SportCode = param.SportCode;
            criteria.LanguageCode = param.LanguageCode;
            criteria.TotalSignUps = param.TotalSignUps;
            criteria.TotalFdms = param.TotalFdms;
            criteria.IsInternal = param.IsInternal;
        }
    }
}
