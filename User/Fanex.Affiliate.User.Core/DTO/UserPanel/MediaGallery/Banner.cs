using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery
{
    public class Banner
    {
        public int BannerId { get; set; }

        public string FileName { get; set; }

        public string TargetProduct { get; set; }

        public string Language { get; set; }

        public string Size { get; set; }

        public DateTime LastUpdate { get; set; }

        public string SelectedDomainName { get; set; }

        public string RedirectLink { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Message { get; set; }

        public string ScriptSrc { get; set; }
    }
}
