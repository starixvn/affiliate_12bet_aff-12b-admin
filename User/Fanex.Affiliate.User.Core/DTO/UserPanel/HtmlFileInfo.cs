using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class HtmlFileInfo
    {
        public string FileName { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public string Language { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
