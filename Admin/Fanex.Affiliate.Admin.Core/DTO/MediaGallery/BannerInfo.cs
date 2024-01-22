using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class BannerInfo
    {
        public int BannerId { get; set; }

        public string FileName { get; set; }

        public string[] TargetProductIds { get; set; }

        public string[] TargetProductNames { get; set; }

        public string Language { get; set; }

        public string[] AffTypes { get; set; }

        public string Size { get; set; }

        public int NumberOfCopies { get; set; }

        [JsonIgnore]
        public DateTime? LastUpdate { get; set; }

        public string LastUpdateText => LastUpdate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
