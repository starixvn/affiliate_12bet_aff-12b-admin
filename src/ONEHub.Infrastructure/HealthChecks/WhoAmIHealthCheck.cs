using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ONEHub.Infrastructure.HealthChecks;

public class WhoAmIHealthCheck(IHttpContextAccessor httpContextAccessor, IHostEnvironment env) : IHealthCheck
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IHostEnvironment _env = env;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        IDictionary<string, object> result = new Dictionary<string, object>
        {
            { "Env", _env.EnvironmentName }
        };

        AddExtraServerInfo(result);

        return Task.FromResult(HealthCheckResult.Healthy("ONEHub Admin - Server Info", new ReadOnlyDictionary<string, object>(result)));
    }
    private void AddExtraServerInfo(IDictionary<string, object> result)
    {
        var context = _httpContextAccessor.HttpContext;

        if (context != null)
        {
            result.Add("Host", context.Request.Host.Host);
            result.Add("Port", context.Connection.LocalPort);
            result.Add("Server IP", context.Connection.LocalIpAddress?.ToString() ?? "N/A");
            result.Add("Server Time", DateTimeOffset.Now);
            result.Add("Client Ip", context.Connection.RemoteIpAddress?.ToString() ?? "N/A");
            result.Add("Last Updated", File.GetCreationTime(typeof(WhoAmIHealthCheck).Assembly.Location).ToString(CultureInfo.InvariantCulture));
            result.Add("Schema", context.Request.Scheme);
            result.Add("X-Headers", "---------------------------------");

            foreach (var item in context.Request.Headers.Where(i => i.Key.Contains("X-", StringComparison.OrdinalIgnoreCase)))
            {
                result.Add($" + {item.Key}", JsonConvert.SerializeObject(item.Value));
            }
        }
        else
        {
            result.Add("Server Time", DateTimeOffset.Now);
        }
    }
}
