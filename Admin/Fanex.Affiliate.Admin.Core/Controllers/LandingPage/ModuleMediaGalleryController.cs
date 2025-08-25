using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Configurations;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using System;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.LandingPage)]
    public class ModuleMediaGalleryController : ReadOnlySessionBaseController
    {
        private readonly IModuleMediaGalleryService _mediaService;
        private readonly IModuleManagementService _moduleService;
        private readonly IAppSetting _appSetting;
        private readonly ISystemService _systemService;

        public ModuleMediaGalleryController(
            ISessionService sessionService,
            IModuleMediaGalleryService mediaService,
            IModuleManagementService moduleService,
            IAppSetting appSetting,
            ISystemService systemService) : base(sessionService)
        {
            _mediaService = mediaService;
            _moduleService = moduleService;
            _appSetting = appSetting;
            _systemService = systemService;
        }
        [HttpGet]
        public ActionResult Index(int? moduleId)
        {
            var model = new ModuleMediaGalleryViewModel
            {
                ModuleList = _moduleService.GetModuleList(),
                MediaRootUrl = _appSetting.BannerRootUrl, // Reusing banner root URL for now
                SelectedModuleId = moduleId ?? 0,
                ModuleTypes = _moduleService.GetModuleTypes()
            };
            return View("~/Areas/Portal/Views/MediaGallery/ModuleMediaGallery.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetModuleMediaList(ModuleMediaParam param)
        {
            if (param.ModuleId <= 0)
            {
                return GenerateErrResult("Invalid module ID.");
            }

            var criteria = new ModuleMediaCriteria
            {
                ModuleId = param.ModuleId,
                SearchTerm = param.SearchTerm,
                FileType = param.FileType,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                IncludeDeleted = false
            };

            return GenerateJsonNetResult(_mediaService.GetModuleMediaList(criteria));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMediaById(int mediaId)
        {
            if (mediaId <= 0)
            {
                return GenerateErrResult("Invalid media ID.");
            }

            return GenerateJsonNetResult(_mediaService.GetMediaById(mediaId));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMediaByModule(int moduleId)
        {
            if (moduleId <= 0)
            {
                return GenerateErrResult("Invalid module ID.");
            }

            return GenerateJsonNetResult(_mediaService.GetMediaByModuleId(moduleId));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult UploadModuleMedia(HttpPostedFileBase[] files, UploadModuleMediaParam param)
        {
            if (ModelState.IsValid && files != null && files.Length > 0)
            {
                var encodeParam = EncodeMediaParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_mediaService.UploadModuleMedia(files, encodeParam));
            }
            return GenerateErrResult("Please select valid files to upload.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult UpdateModuleMedia(HttpPostedFileBase file, UpdateModuleMediaParam param)
        {
            if (ModelState.IsValid && param.MediaId > 0)
            {
                var encodeParam = EncodeUpdateMediaParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_mediaService.UpdateModuleMedia(file, encodeParam));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult DeleteModuleMedia(DeleteModuleMediaParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_mediaService.DeleteModuleMedia(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ValidateMediaFile(HttpPostedFileBase file, int moduleId, int moduleType)
        {
            if (file == null)
            {
                return GenerateErrResult("No file provided for validation.");
            }

            // Get module information for specific validation
            var moduleInfo = _moduleService.GetModuleById(moduleId);
            if (moduleInfo == null && moduleId > 0)
            {
                return GenerateErrResult("Invalid module ID.");
            }

            var isValid = _mediaService.ValidateMediaFile(file, moduleType);
            var extension = System.IO.Path.GetExtension(file.FileName);
            
            var result = new
            {
                IsValid = isValid,
                FileName = file.FileName,
                FileSize = file.ContentLength,
                FileSizeFormatted = FormatFileSize(file.ContentLength),
                ContentType = file.ContentType,
                Extension = extension,
                IsImage = Utilities.MediaGallery.BannerHelper.IsValidExtension(extension),
                ModuleName = moduleInfo?.ModuleName ?? "Unknown",
                ModuleType = moduleType,
                ValidationMessage = isValid ? "File is valid" : GetValidationMessage(file, moduleType, moduleInfo),
                MaxFileSize = "5 MB",
                AllowedTypes = GetAllowedTypesForModule(moduleType)
            };

            return GenerateJsonNetResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMediaRootUrl()
        {
            return GenerateJsonNetResult(_appSetting.BannerRootUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetModuleMediaStats(int moduleId)
        {
            if (moduleId <= 0)
            {
                return GenerateErrResult("Invalid module ID.");
            }

            var media = _mediaService.GetMediaByModuleId(moduleId);
            var stats = new
            {
                TotalFiles = media.Count(),
                TotalSize = media.Sum(m => GetFileSizeFromString(m.Size)),
                FileTypes = media.GroupBy(m => m.FileExtension)
                    .Select(g => new { Extension = g.Key, Count = g.Count() })
                    .ToArray()
            };

            return GenerateJsonNetResult(stats);
        }

        private UploadModuleMediaParam EncodeMediaParam(UploadModuleMediaParam param)
        {
            return new UploadModuleMediaParam
            {
                ModuleId = param.ModuleId,
                ModuleType = param.ModuleType,
                MediaName = Server.HtmlEncode(param.MediaName),
                Size = Server.HtmlEncode(param.Size)
            };
        }

        private UpdateModuleMediaParam EncodeUpdateMediaParam(UpdateModuleMediaParam param)
        {
            return new UpdateModuleMediaParam
            {
                MediaId = param.MediaId,
                MediaName = Server.HtmlEncode(param.MediaName),
                Size = Server.HtmlEncode(param.Size)
            };
        }

        private long GetFileSizeFromString(string sizeString)
        {
            // Parse size string like "100x200" or file size
            if (string.IsNullOrEmpty(sizeString))
            {
                return 0;
            }

            // This is a simplified implementation
            // In real scenario, you might want to store actual file sizes
            return sizeString.Length * LandingPageConstants.FileUpload.BytesPerKilobyte; // Placeholder calculation
        }

        private string FormatFileSize(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB" };
            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number / LandingPageConstants.FileUpload.BytesPerKilobyte) >= 1)
            {
                number = number / LandingPageConstants.FileUpload.BytesPerKilobyte;
                counter++;
            }
            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }

        private string GetValidationMessage(HttpPostedFileBase file, int moduleType, dynamic moduleInfo)
        {
            if (file.ContentLength > LandingPageConstants.FileUpload.MaxFileSizeBytes)
            {
                return $"File size ({FormatFileSize(file.ContentLength)}) exceeds {FormatFileSize(LandingPageConstants.FileUpload.MaxFileSizeBytes)} limit";
            }
                
            var extension = System.IO.Path.GetExtension(file.FileName);
            if (!Utilities.MediaGallery.BannerHelper.IsValidExtension(extension))
            {
                return $"File type {extension} is not allowed. Allowed types: {string.Join(", ", GetAllowedTypesForModule(moduleType))}";
            }

            // Module-specific validation messages
            switch (moduleType)
            {
                case LandingPageConstants.ModuleTypes.Image:
                    return ValidateImageFileMessage(file, moduleInfo);
                case LandingPageConstants.ModuleTypes.Link:
                    return LandingPageConstants.ErrorMessages.LinkModuleNoUpload;
                default:
                    return "File validation failed";
            }
        }

        private string ValidateImageFileMessage(HttpPostedFileBase file, dynamic moduleInfo)
        {
            try
            {
                using (var image = System.Drawing.Image.FromStream(file.InputStream))
                {
                    var maxWidth = GetMaxWidthForModule(moduleInfo);
                    var maxHeight = GetMaxHeightForModule(moduleInfo);
                    
                    if (image.Width > maxWidth || image.Height > maxHeight)
                    {
                        return $"Image dimensions ({image.Width}x{image.Height}) exceed maximum allowed size ({maxWidth}x{maxHeight}) for {moduleInfo?.ModuleName ?? "this module"}";
                    }
                }
                file.InputStream.Position = 0; // Reset stream
            }
            catch
            {
                return "Invalid image file or corrupted data";
            }

            return "Image is valid";
        }

        private string[] GetAllowedTypesForModule(int moduleType)
        {
            switch (moduleType)
            {
                case LandingPageConstants.ModuleTypes.Image:
                case LandingPageConstants.ModuleTypes.Contact:
                case LandingPageConstants.ModuleTypes.Form:
                    return LandingPageConstants.FileExtensions.AllowedImageExtensions;
                case LandingPageConstants.ModuleTypes.Link:
                    return new string[0]; // No file uploads allowed
                default:
                    return LandingPageConstants.FileExtensions.AllowedImageExtensions;
            }
        }

        private int GetMaxWidthForModule(dynamic moduleInfo)
        {
            if (moduleInfo == null) return LandingPageConstants.FileUpload.MaxImageWidth;

            var moduleName = (string)moduleInfo.ModuleName ?? "";
            
            switch (moduleName.ToLowerInvariant())
            {
                case LandingPageConstants.ModuleNames.Background:
                    return LandingPageConstants.ModuleImageLimits.BackgroundMaxWidth;
                case LandingPageConstants.ModuleNames.HeroImage:
                    return LandingPageConstants.ModuleImageLimits.HeroMaxWidth;
                case LandingPageConstants.ModuleNames.LogoSponsor1:
                case LandingPageConstants.ModuleNames.LogoSponsor2:
                    return LandingPageConstants.ModuleImageLimits.LogoMaxWidth;
                case LandingPageConstants.ModuleNames.CTAButton:
                    return LandingPageConstants.ModuleImageLimits.ButtonMaxWidth;
                default:
                    return LandingPageConstants.FileUpload.MaxImageWidth;
            }
        }

        private int GetMaxHeightForModule(dynamic moduleInfo)
        {
            if (moduleInfo == null) return LandingPageConstants.FileUpload.MaxImageHeight;

            var moduleName = (string)moduleInfo.ModuleName ?? "";
            
            switch (moduleName.ToLowerInvariant())
            {
                case LandingPageConstants.ModuleNames.Background:
                    return LandingPageConstants.ModuleImageLimits.BackgroundMaxHeight;
                case LandingPageConstants.ModuleNames.HeroImage:
                    return LandingPageConstants.ModuleImageLimits.HeroMaxHeight;
                case LandingPageConstants.ModuleNames.LogoSponsor1:
                case LandingPageConstants.ModuleNames.LogoSponsor2:
                    return LandingPageConstants.ModuleImageLimits.LogoMaxHeight;
                case LandingPageConstants.ModuleNames.CTAButton:
                    return LandingPageConstants.ModuleImageLimits.ButtonMaxHeight;
                default:
                    return LandingPageConstants.FileUpload.MaxImageHeight;
            }
        }
    }
}
