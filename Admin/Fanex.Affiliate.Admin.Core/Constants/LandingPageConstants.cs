using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class LandingPageConstants
    {
        // Module Types
        public static class ModuleTypes
        {
            public const int Image = 1;
            public const int Contact = 2;
            public const int Form = 3;
            public const int Link = 4;
            public const int Login = 5;
        }

        public static class ModuleIds
        {
            public const int RegistrationForm = 9;
        }

        // File Upload Limits
        public static class FileUpload
        {
            public const int MaximumFiles = 10;
            public const int BytesPerKilobyte = 1024;
            public const int MaxFileSizeBytes = 3 * BytesPerKilobyte * BytesPerKilobyte; // 3MB
            public const int MaxFileSizeMegabytes = 3;
            public const int MaxImageWidth = 2000;
            public const int MaxImageHeight = 2000;
            public const int ThumbnailSize = 150;
            public const string DefaultMediaFolder = "~/App_Data/Media";
        }

        // Module-Specific Image Limits
        public static class ModuleImageLimits
        {
            // Background Module
            public const int BackgroundMaxWidth = 1920;
            public const int BackgroundMaxHeight = 1080;

            // Hero Image Module
            public const int HeroMaxWidth = 1200;
            public const int HeroMaxHeight = 600;

            // Logo & Sponsor Module
            public const int LogoMaxWidth = 400;
            public const int LogoMaxHeight = 200;

            // CTA Button Module
            public const int ButtonMaxWidth = 300;
            public const int ButtonMaxHeight = 100;
        }

        // Folder Structure
        public static class FolderStructure
        {
            public const string LandingPageRoot = "LandingPage";
            public const string ImagesFolder = "Images";
            public const string ContactFolder = "Contact";
            public const string FormsFolder = "Forms";
            public const string LinksFolder = "Links";
            public const string ThumbnailsFolder = "thumbnails";
            public const string GeneralFolder = "General";
        }

        // File Extensions
        public static class FileExtensions
        {
            public static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            public static readonly string[] AllImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        }

        // Module Names (for validation)
        public static class ModuleNames
        {
            public const string Background = "background";
            public const string HeroImage = "hero image";
            public const string LogoSponsor1 = "logo & sponsor";
            public const string LogoSponsor2 = "logo sponsor";
            public const string CTAButton = "cta button";
        }

        // Validation Limits
        public static class ValidationLimits
        {
            public const int StandardTextLength = 200;
            public const int MaxPageSize = 2000;
            public const int ModuleTypeId = 4;
        }

        // Error Messages
        public static class ErrorMessages
        {
            public const string NoFileProvided = "No file provided or file is empty";
            public const string InvalidModuleId = "Invalid module ID";
            public const string FileSizeExceeded = "File size exceeds maximum limit";
            public const string InvalidFileType = "File type is not allowed";
            public const string ImageDimensionsExceeded = "Image dimensions exceed maximum allowed size";
            public const string InvalidImageData = "Invalid image file or corrupted data";
            public const string LinkModuleNoUpload = "Link modules do not support file uploads";
            public const string MediaNotFound = "Media not found";
        }

        // Success Messages
        public static class SuccessMessages
        {
            public const string FileValid = "File is valid";
            public const string ImageValid = "Image is valid";
            public const string UploadSuccess = "Successfully uploaded {0} of {1} files";
            public const string UploadFailed = "Failed to upload any files";
        }

        public static class AssetStatusTypes
        {
            public const int Active = 1;
            public const int Inactive = 2;
        }

        public static IList<StatusInfo> AssetStatusList => new List<StatusInfo>
        {
            new StatusInfo { StatusId = AssetStatusTypes.Active, Status = "Active" },
            new StatusInfo { StatusId = AssetStatusTypes.Inactive, Status = "Inactive" },
        };
    }
}
