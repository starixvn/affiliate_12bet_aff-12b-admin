using System;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class LandingPageAsset
    {
        public int MediaId { get; set; }
        public string MediaName { get; set; }
        public string FileName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Dimension { get; set; }
        public byte StatusId { get; set; }
        public string TargetCountryCode { get; set; }
        public string TargetCountryName { get; set; }
        public string MarketId { get; set; }
        public string MarketName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsMediaIdInUse { get; set; }
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
