using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery
{
    public class ImageSizeViewModel
    {
        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public string ImageType { get; set; }

        public byte AffType { get; set; } = Constants.AffType.Asian;

        public byte DeviceType { get; set; }
    }
}
