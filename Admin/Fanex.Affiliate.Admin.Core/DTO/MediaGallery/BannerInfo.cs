using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class BannerInfo
    {
        public int BannerId { get; set; }

        public string FileName { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public byte DeviceType { get; set; }

        public string ImageType { get; set; }

        public byte AffType { get; set; }

        public string Size { get; set; }

        [JsonIgnore]
        public DateTime? LastUpdate { get; set; }

        public string LastUpdateText => LastUpdate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
