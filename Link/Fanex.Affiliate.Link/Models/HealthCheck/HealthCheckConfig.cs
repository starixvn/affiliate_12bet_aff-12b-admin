namespace Fanex.Affiliate.Link.Models.HealthCheck
{
    /// <summary>
    /// Configuration settings for health check system
    /// </summary>
    public class HealthCheckConfig
    {
        public bool Enabled { get; set; } = true;
        public bool RequireAuthentication { get; set; } = false;
        public string ApiKey { get; set; }
        public int TimeoutSeconds { get; set; } = 30;
        public int CacheDurationSeconds { get; set; } = 60;
        public bool EnableDetailedLogging { get; set; } = true;
        public bool EnableAlerts { get; set; } = false;
        public HealthThresholds Thresholds { get; set; } = new();
        public Dictionary<string, bool> ComponentsEnabled { get; set; } = new()
        {
            ["database"] = true,
            ["redis"] = true,
            ["rabbitmq"] = true,
            ["route_constraints"] = true,
            ["maintenance_service"] = true
        };
    }

    public class HealthThresholds
    {
        public double DatabaseResponseTimeMs { get; set; } = 1000;
        public double RedisResponseTimeMs { get; set; } = 100;
        public double RouteConstraintResponseTimeMs { get; set; } = 1;
        public long MemoryUsageMB { get; set; } = 500;
        public int ActiveThreadsWarning { get; set; } = 100;
        public int ActiveThreadsCritical { get; set; } = 200;
    }
}