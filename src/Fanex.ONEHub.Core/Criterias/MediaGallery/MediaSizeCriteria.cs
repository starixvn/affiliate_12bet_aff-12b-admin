namespace Fanex.Affiliate.Admin.Core.Criterias.MediaGallery
{
    public class MediaSizeCriteria : BaseBannerCriteria
    {
        const string SP_NAME = "Aff_Admin_MediaSize_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string ImageType { get; set; }
    }
}
