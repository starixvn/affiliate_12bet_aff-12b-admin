using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Criterias.MediaGallery
{
    public class BannerCriteria : BaseBannerCriteria
    {
        const string SP_NAME = "Aff_Admin_Banner_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string Size { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        [SpParam(SqlParamName = "NRows")]
        public int TotalItem { get; set; }
    }
}
