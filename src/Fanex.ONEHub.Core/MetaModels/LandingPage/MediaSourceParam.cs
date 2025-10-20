using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class MediaSourceParam
    {
        public int ModuleId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TargetCountryCode { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LanguageCode { get; set; }
    }
}
