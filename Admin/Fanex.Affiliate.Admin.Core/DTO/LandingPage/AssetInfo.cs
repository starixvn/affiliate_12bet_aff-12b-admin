using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class AssetInfo
    {
        public int MediaId { get; set; }

        public string MediaName { get; set; }

        public string FileName { get; set; }

        public string FullPath => !string.IsNullOrEmpty(MediaRootUrl) && !string.IsNullOrEmpty(FileName) 
            ? $"{MediaRootUrl.TrimEnd('/')}/{FileName.Replace('\\', '/')}" // Ensure forward slashes for URLs
            : FileName;

        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public string Size { get; set; }

        public int Creator { get; set; }

        public string CreatorName { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string MediaRootUrl { get; set; }

        public string FileExtension => !string.IsNullOrEmpty(FileName) 
            ? System.IO.Path.GetExtension(FileName).ToLowerInvariant() 
            : string.Empty;

        public bool IsImage
        {
            get
            {
                var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                return Array.IndexOf(imageExtensions, FileExtension) >= 0;
            }
        }

        public string PreviewUrl => IsImage ? FullPath : GetDefaultPreviewPath();

        // Add thumbnail path property for image files
        public string ThumbnailPath
        {
            get
            {
                if (!IsImage || string.IsNullOrEmpty(MediaRootUrl) || string.IsNullOrEmpty(FileName))
                {
                    return FullPath; // Fallback to full path
                }

                var directory = System.IO.Path.GetDirectoryName(FileName)?.Replace('\\', '/');
                var fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(FileName);
                var extension = FileExtension;
                
                return $"{MediaRootUrl.TrimEnd('/')}/{directory}/thumbnails/{fileNameWithoutExt}_thumb{extension}";
            }
        }

        // Add file size in bytes for better sorting and display
        public long FileSizeBytes
        {
            get
            {
                // Try to parse size if it's stored as file size, otherwise return 0
                if (string.IsNullOrEmpty(Size))
                {
                    return 0;
                }

                // If size is stored as dimensions (e.g., "1920x1080"), return 0
                if (Size.Contains("x"))
                {
                    return 0;
                }

                // Try to parse as bytes
                if (long.TryParse(Size, out long bytes))
                {
                    return bytes;
                }

                return 0;
            }
        }

        // Formatted file size for display
        public string FormattedFileSize
        {
            get
            {
                if (FileSizeBytes == 0)
                {
                    return Size ?? "Unknown";
                }

                return FormatFileSize(FileSizeBytes);
            }
        }

        // Get module type based on folder structure
        public int ModuleType
        {
            get
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    return 1; // Default to image
                }

                var path = FileName.ToLowerInvariant();
                if (path.Contains("/images/"))
                {
                    return 1;
                }
                if (path.Contains("/contact/"))
                {
                    return 2;
                }
                if (path.Contains("/forms/"))
                {
                    return 3;
                }
                if (path.Contains("/links/"))
                {
                    return 4;
                }

                return 1; // Default to image
            }
        }

        private string GetDefaultPreviewPath()
        {
            switch (FileExtension)
            {
                case ".pdf":
                    return "/images/icons/pdf-icon.png";
                case ".doc":
                case ".docx":
                    return "/images/icons/doc-icon.png";
                case ".xls":
                case ".xlsx":
                    return "/images/icons/excel-icon.png";
                default:
                    return "/images/icons/file-icon.png";
            }
        }

        private static string FormatFileSize(long bytes)
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
    }
}
