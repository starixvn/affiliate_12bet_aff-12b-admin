using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    public class ModuleMediaGalleryViewModel
    {
        public IEnumerable<LandingPageModule> ModuleList { get; set; }

        public IEnumerable<ModuleTypeInfo> ModuleTypes { get; set; }

        public string MediaRootUrl { get; set; }

        public int SelectedModuleId { get; set; }

        public string SelectedModuleName { get; set; }

        public int TotalMediaFiles { get; set; }

        public long TotalStorageUsed { get; set; }

        public bool CanUpload { get; set; } = true;

        public bool CanEdit { get; set; } = true;

        public bool CanDelete { get; set; } = true;

        public int MaxFileSize { get; set; } = LandingPageConstants.FileUpload.MaxFileSizeBytes;

        public string[] AllowedExtensions { get; set; } = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public int MaxImageWidth { get; set; } = 2000;

        public int MaxImageHeight { get; set; } = 2000;
    }
}
