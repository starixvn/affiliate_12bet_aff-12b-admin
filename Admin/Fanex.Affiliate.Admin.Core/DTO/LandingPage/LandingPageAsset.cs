using System;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class LandingPageAsset
    {
        public int MediaId { get; set; }
        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string Size { get; set; }
        public long FileSize { get; set; }
        public string AssetType { get; set; } // Image, Video, Document, etc.
        public int? ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Tags { get; set; }
        public string Dimensions { get; set; } // Width x Height for images
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        // Helper properties
        public string AssetRootUrl { get; set; }
        public string FullUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string FileSizeFormatted { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public bool IsDocument { get; set; }
    }

    public class AssetStatistics
    {
        public int TotalAssets { get; set; }
        public long TotalSize { get; set; }
        public int ImageCount { get; set; }
        public int VideoCount { get; set; }
        public int DocumentCount { get; set; }
        public string TotalSizeFormatted { get; set; }
        public DateTime LastUploadDate { get; set; }
    }

    public class OptimizedAssetResult
    {
        public bool WasOptimized { get; set; }
        public string OriginalFileName { get; set; }
        public string OptimizedFileName { get; set; }
        public long OriginalSize { get; set; }
        public long OptimizedSize { get; set; }
        public string OriginalDimensions { get; set; }
        public string OptimizedDimensions { get; set; }
        public int CompressionPercentage { get; set; }
        public string OptimizedFilePath { get; set; }
    }
}
