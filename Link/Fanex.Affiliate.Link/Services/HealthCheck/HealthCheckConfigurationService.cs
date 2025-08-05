namespace Fanex.Affiliate.Link.Services.HealthCheck
{
    using Microsoft.Extensions.Options;
    using Models.HealthCheck;

    /// <summary>
    /// Configuration service for health check system
    /// </summary>
    public class HealthCheckConfigurationService : IHealthCheckConfigurationService
    {
        private readonly HealthCheckConfig _config;
        private readonly ILogger<HealthCheckConfigurationService> _logger;
        private readonly IConfiguration _configuration;

        public HealthCheckConfigurationService(
            IOptions<HealthCheckConfig> config,
            ILogger<HealthCheckConfigurationService> logger,
            IConfiguration configuration)
        {
            _config = config.Value ?? new HealthCheckConfig();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public HealthCheckConfig GetConfiguration() => _config;

        public bool IsComponentEnabled(string componentName)
        {
            return _config.ComponentsEnabled.TryGetValue(componentName.ToLowerInvariant(), out var enabled) && enabled;
        }

        public bool IsAuthenticationRequired() => _config.RequireAuthentication;

        public bool ValidateApiKey(string apiKey)
        {
            if (!_config.RequireAuthentication) return true;
            if (string.IsNullOrEmpty(_config.ApiKey)) return false;
            return string.Equals(_config.ApiKey, apiKey, StringComparison.Ordinal);
        }

        public HealthThresholds GetThresholds() => _config.Thresholds ?? new HealthThresholds();

        public Task<bool> SendAlertAsync(string componentName, string message, string severity = "Warning")
        {
            if (!_config.EnableAlerts)
            {
                _logger.LogInformation("Alert not sent - alerts disabled or no email addresses configured");
                return Task.FromResult(false);
            }

            try
            {
                var subject = $"[{severity}] Health Check Alert - {componentName}";
                var body = $@"
Health Check Alert

Component: {componentName}
Severity: {severity}
Message: {message}
Timestamp: {DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm:ss} UTC
Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"}
Server: {Environment.MachineName}

This is an automated health check alert from the Fanex Affiliate Link system.
";

                _logger.LogWarning("HEALTH ALERT: {Subject}\n{Body}", subject, body);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send health check alert for component {Component}", componentName);
                return Task.FromResult(false);
            }
        }
    }
}