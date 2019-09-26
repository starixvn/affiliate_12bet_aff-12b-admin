using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class BannerInfo
    {
        public int BannerId { get; set; }

        public string Images { get; set; }

        public string FileName { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public string DeviceType { get; set; }

        public string ImageType { get; set; }

        public string Size { get; set; }
    }
}
