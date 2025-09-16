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
            public const int MaxImageHeight = 5000;
            public const int ThumbnailSize = 150;
            public const string DefaultMediaFolder = "~/App_Data/Media";
        }

        // File Extensions
        public static class FileExtensions
        {
            public static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            public static readonly string[] AllImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
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

        public static class CampaignStatusTypes
        {
            public const int Draft = 0;
            public const int Active = 1;
            public const int Paused = 2;
            public const int Inactive = 3;
        }

        public static IList<StatusInfo> AssetStatusList => new List<StatusInfo>
        {
            new StatusInfo { StatusId = AssetStatusTypes.Active, Status = "Active" },
            new StatusInfo { StatusId = AssetStatusTypes.Inactive, Status = "Inactive" },
        };
    }
}
