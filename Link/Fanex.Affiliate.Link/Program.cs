using Fanex.Affiliate.Link.Extensions;
using Fanex.Affiliate.Link.Filters;
using Fanex.Affiliate.Link.Middleware;
using Fanex.Affiliate.Link.Services.Impl;
using Fanex.Affiliate.Link.Services.Interface;
using Fanex.Caching;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure route options for better performance
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = false;
});

// Configure logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Register custom services
builder.Services.AddScoped<ITrackingSystemService, TrackingSystemService>();
builder.Services.AddScoped<IAffInfoService, AffInfoService>();
builder.Services.AddScoped<IMemoryCacheService, MemoryCacheService>();
builder.Services.AddScoped<IUnderMaintenanceService, UnderMaintenanceService>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddSingleton<IDynamicRepository, DynamicRepository>();

// Register action filters for the Hybrid Approach
builder.Services.AddScoped<AffiliateValidationFilter>();


// Configure configuration
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

//var redis = ConnectionMultiplexer.Connect(
//                builder.Configuration.GetSection("fanex.caching.redis:ConnectionOptions:EndPoints").Value);

builder.Services.AddRedisCache(builder.Configuration.GetSection("fanex.caching.redis"));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Custom middleware for handling affiliate link 404 errors with detailed messages
app.UseMiddleware<AffiliateErrorMiddleware>();

// Default route for standard MVC actions
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure affiliate link routes using centralized extension method
app.MapAffiliateRoutes();

// Initialize Fanex Data
DbSettingProviderManager
    .StartNewSession()
    .Register(builder.Configuration)
    .Run();



// Register Sentry logging
Fanex.Affiliate.Link.LoggingConfig.RegisterSentry();

app.Run();