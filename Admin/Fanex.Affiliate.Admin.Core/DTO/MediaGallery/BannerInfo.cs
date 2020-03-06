using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class BannerInfo
    {
        public int BannerId { get; set; }

        public string FileName { get; set; }

        public string[] Categories { get; set; }

        public string[] SubCategories { get; set; }

        public string Language { get; set; }

        public string[] Currencies { get; set; }

        public string[] DeviceTypes { get; set; }

        public string ImageType { get; set; }

        public string[] AffTypes { get; set; }

        public string Size { get; set; }

        [JsonIgnore]
        public DateTime? LastUpdate { get; set; }

        public string LastUpdateText => LastUpdate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
