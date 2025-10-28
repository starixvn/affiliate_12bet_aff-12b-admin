using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using ONEHub.Infrastructure.HealthChecks;
using System.Net.Mime;

namespace ONEHub.WebApi.Extensions;

public static class HealthCheckExtensions
{
    private static readonly string[] Tags = ["ready"];

    public static IHealthChecksBuilder AddSiteHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSetting = configuration.GetSection("fanex.caching.redis");

        var healthCheckBuilder = services.AddHealthChecks()
            .AddCheck<WhoAmIHealthCheck>("WhoAmI");

        var redisConnectionString = redisSetting.GetValue<string>("ConnectionString");
        if (!string.IsNullOrEmpty(redisConnectionString))
        {
            healthCheckBuilder.AddRedis(
                redisConnectionString,
                name: "Redis",
                timeout: TimeSpan.FromSeconds(5),
                tags: Tags);
        }

        return healthCheckBuilder;
    }

    public static void UseSiteHealthChecks(this IApplicationBuilder app, IConfiguration configuration)
    {
        static async Task CustomResponseWriter(HttpContext context, HealthReport report)
        {
            var result = JsonConvert.SerializeObject(
                new
                {
                    status = report.Status.ToString(),
                    monitors = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = Enum.GetName(e.Value.Status),
                        values = e.Value.Data
                    })
                }, Formatting.Indented);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(result);
        }

        var accessKey = configuration["HealthCheck:AccessKey"];

        app.UseWhen(context =>
            string.IsNullOrEmpty(accessKey) ||
            (context.Request.Query.TryGetValue("AccessKey", out var value) && accessKey.Equals(value, StringComparison.Ordinal)),
            builder => builder
                .UseHealthChecks("/healthz", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    Predicate = _ => _.Name.Equals("WhoAmI", StringComparison.Ordinal),
                    ResponseWriter = CustomResponseWriter
                }).UseHealthChecks("/healthcheck", options: new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    Predicate = _ => !_.Name.Equals("WhoAmI", StringComparison.Ordinal),
                    ResponseWriter = CustomResponseWriter
                }));
    }
}
