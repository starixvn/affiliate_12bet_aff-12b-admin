using Fanex.Data;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.MediaGallery
{
    public abstract class BaseBannerCriteria : CriteriaBase
    {
        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public byte AffType { get; set; }

        public string DeviceType { get; set; }

        [SpParam(SqlParamName = "TargetId")]
        public byte UserType { get; set; }
    }
}
