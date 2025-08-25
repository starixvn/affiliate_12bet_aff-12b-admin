using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class MediaStatistics
    {
        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public int TotalFiles { get; set; }

        public long TotalSizeBytes { get; set; }

        public string TotalSizeFormatted { get; set; }

        public int ImageFiles { get; set; }

        public int VideoFiles { get; set; }

        public int DocumentFiles { get; set; }

        public int OtherFiles { get; set; }

        public IEnumerable<FileTypeStatistic> FileTypeBreakdown { get; set; }

        public string MostUsedFileType { get; set; }

        public string LargestFile { get; set; }

        public long LargestFileSize { get; set; }
    }

    public class FileTypeStatistic
    {
        public string Extension { get; set; }

        public int Count { get; set; }

        public long TotalSize { get; set; }

        public string TotalSizeFormatted { get; set; }

        public double Percentage { get; set; }
    }
}
