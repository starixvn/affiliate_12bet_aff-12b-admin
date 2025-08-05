namespace Fanex.Affiliate.Link.Models.HealthCheck
{
    /// <summary>
    /// System performance metrics
    /// </summary>
    public class SystemMetrics
    {
        public double CpuUsagePercent { get; set; }
        public long MemoryUsedBytes { get; set; }
        public long MemoryTotalBytes { get; set; }
        public double MemoryUsagePercent => MemoryTotalBytes > 0 ? (double)MemoryUsedBytes / MemoryTotalBytes * 100 : 0;
        public long DiskUsedBytes { get; set; }
        public long DiskTotalBytes { get; set; }
        public double DiskUsagePercent => DiskTotalBytes > 0 ? (double)DiskUsedBytes / DiskTotalBytes * 100 : 0;
        public int ActiveThreads { get; set; }
        public int ActiveConnections { get; set; }
        public TimeSpan Uptime { get; set; }
        public string Version { get; set; }
        public string Environment { get; set; }

        public Dictionary<string, object> AdditionalMetrics { get; set; } = new();
    }
}