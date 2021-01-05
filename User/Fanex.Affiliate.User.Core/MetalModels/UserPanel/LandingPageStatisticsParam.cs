using Fanex.Data;
using System;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    [Serializable]
    public class LandingPageStatisticsParam : StatisticsBySiteBaseParam
    {
        public string SportCode { get; set; }

        [SpParam(Ignored = true)]
        public string LanguageCode { get; set; }

        public string LanguageCodes { get; set; }

        public int TotalSignUps { get; set; }

        public int TotalFdms { get; set; }
    }
}
