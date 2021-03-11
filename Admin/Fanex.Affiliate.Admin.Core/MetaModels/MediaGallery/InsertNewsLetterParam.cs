using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class InsertNewsLetterParam
    {
        public int UserId { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public string Language { get; set; }

        public string Ip { get; set; }
    }
}
