using Fanex.Affiliate.Link.Services.Impl;
using Fanex.Affiliate.Link.Services.Interface;
using Fanex.Caching;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Register legacy routes
app.MapControllerRoute(
    name: "OldLink",
    pattern: "{affId}/{pageCode}/{lang}/index.html",
    defaults: new { controller = "Home", action = "OldLink" },
    constraints: new { affId = @"\d+", lang = @"(\w{2}|hien|hitg|eneuro|csmy|mycs)" });

app.MapControllerRoute(
    name: "SpecialPageCodeOldLink",
    pattern: "{affId}/{pageCodePart1}/{pageCodePart2}/{lang}/index.html",
    defaults: new { controller = "Home", action = "SpecialPageCodeOldLink" },
    constraints: new { affId = @"\d+", lang = @"(\w{2}|hien|hitg|eneuro|csmy|mycs)" });

app.MapControllerRoute(
    name: "ChannelLink",
    pattern: "{pageCode}/{affCode}/{lang}/{channel}/index.html",
    defaults: new { controller = "Home", action = "Link" },
    constraints: new { affCode = @"\d+", lang = @"(\w{2}|hien|hitg|eneuro|csmy|mycs)" });

app.MapControllerRoute(
    name: "SpecialPageCodeChannelLink",
    pattern: "{pageCodePart1}/{pageCodePart2}/{affCode}/{lang}/{channel}/index.html",
    defaults: new { controller = "Home", action = "SpecialPageCodeLink" },
    constraints: new { affCode = @"\d+", lang = @"(\w{2}|hien|hitg|eneuro|csmy|mycs)" });

app.MapControllerRoute(
    name: "LinkWithoutChannel",
    pattern: "{pageCode}/{affCode}/{lang}/index.html",
    defaults: new { controller = "Home", action = "Link" },
    constraints: new { affCode = @"\d+", lang = @"(\w{2}|hien|hitg|eneuro|csmy|mycs)" });

// Initialize Fanex Data
DbSettingProviderManager
    .StartNewSession()
    .Register(builder.Configuration)
    .Run();



// Register Sentry logging
Fanex.Affiliate.Link.LoggingConfig.RegisterSentry();

app.Run(); 