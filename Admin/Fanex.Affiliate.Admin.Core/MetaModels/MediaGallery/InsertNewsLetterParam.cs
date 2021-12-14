using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertNewsLetterParam : UserInfoParam
    {
        public string FileName { get; set; }

        public int FileSize { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public string Language { get; set; }
    }
}
