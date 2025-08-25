namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class OptimizedImageResult
    {
        public string OptimizedFileName { get; set; }

        public string OriginalFileName { get; set; }

        public long OriginalSizeBytes { get; set; }

        public long OptimizedSizeBytes { get; set; }

        public int OriginalWidth { get; set; }

        public int OriginalHeight { get; set; }

        public int OptimizedWidth { get; set; }

        public int OptimizedHeight { get; set; }

        public double CompressionRatio { get; set; }

        public long SizeSavedBytes { get; set; }

        public string OptimizedFilePath { get; set; }

        public bool WasOptimized { get; set; }

        public string OptimizationMessage { get; set; }
    }
}
