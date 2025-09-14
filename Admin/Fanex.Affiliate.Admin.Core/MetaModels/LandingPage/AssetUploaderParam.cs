using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class AssetUploaderParam : PagingParam
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ModuleIds { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TargetCountrycodes { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LanguageCodes { get; set; }
        public int Status { get; set; }
        public int MediaId { get; set; }
        public string MediaName { get; set; }
        public int UserId { get; set; }
        public string FunctionName { get; set; }
    }
}
