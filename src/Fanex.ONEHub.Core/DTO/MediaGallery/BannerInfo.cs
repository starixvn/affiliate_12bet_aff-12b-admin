using System;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class BannerInfo
    {
        public int BannerId { get; set; }

        public string FileName { get; set; }

        public string Category { get; set; }

        public string[] Categories => Category.Split(',');

        public string SubCategory { get; set; }

        public string[] SubCategories => SubCategory.Split(',');

        public string Language { get; set; }

        public string Currency { get; set; }

        public string[] Currencies => Currency.Split(',');

        public string DeviceType { get; set; }

        public string[] DeviceTypes => DeviceType.Split(',');

        public string AffType { get; set; }

        public string[] AffTypes => AffType.Split(',');

        public string Size { get; set; }

        public byte UserType { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string LastUpdateText => UpdatedDate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}