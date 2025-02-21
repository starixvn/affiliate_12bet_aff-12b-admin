using Fanex.Data;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.MediaGallery
{
    public class BannerDetailCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_Banner_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => BannerId > 0;

        public int BannerId { get; set; }

        [SpParam(SqlParamName = "TargetId")]
        public byte UserType { get; set; }
    }
}
