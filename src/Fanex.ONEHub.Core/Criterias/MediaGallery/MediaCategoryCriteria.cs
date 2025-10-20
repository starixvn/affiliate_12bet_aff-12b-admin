using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.MediaGallery
{
    public class MediaCategoryCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_MediaCategory_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public byte TypeId { get; set; }

        public byte TargetId { get; set; }
    }
}
