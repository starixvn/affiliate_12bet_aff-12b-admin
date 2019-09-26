using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class BannerParam
    {
        public string Category { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public string ImageType { get; set; }

        public string Size { get; set; }

        public byte AffType { get; set; }

        public byte DeviceType { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
