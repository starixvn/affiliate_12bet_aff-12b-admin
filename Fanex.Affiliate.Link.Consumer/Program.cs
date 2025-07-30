using EasyNetQ;
using Fanex.Affiliate.Link.Consumer;
using Fanex.Affiliate.Link.Core.Services.Interfaces;
using Fanex.Data;
using Fanex.Data.Repository;
using Fanex.Logging;
using Fanex.Logging.Sentry;
using ONEbook.UM.DependencyInjection;
using ONEbook.UM.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<RabbitMqWorker>();
var rabbitMqConnectionString = builder.Configuration.GetValue<string>("RabbitMQConnectionString");
builder.Services.AddHttpClient();
builder.Services.AddSingleton(RabbitHutch.CreateBus(rabbitMqConnectionString));
builder.Services.AddOneBookUmForService(builder.Configuration);
builder.Services.AddSingleton<IMaintenanceService, MaintenanceService>();
builder.Services.AddSingleton<IDynamicRepository, DynamicRepository>();
builder.Services.AddSingleton<ILogService, Fanex.Affiliate.Link.Core.Services.LogService>();

var env = builder.Configuration["Sentry:Environment"];
var backEndDsn = builder.Configuration["Sentry:Dsn"];
var category = builder.Configuration["Sentry:LogCategory"];

LogManager.SetDefaultLogCategory(category).Use(new List<ILogEngine>
        {
            new SentryLogging(new SentryEngineOptions
            {
                Dsn = backEndDsn,
                Environment = env
            })
        });

DbSettingProviderManager
    .StartNewSession()
    .Register(builder.Configuration)
    .Run();

var host = builder.Build();
host.Run();
