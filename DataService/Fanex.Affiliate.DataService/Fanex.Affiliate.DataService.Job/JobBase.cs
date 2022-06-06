using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Log;
using Fanex.Caching;
using ONEbook.UM;
using ONEbook.UM.Configurations;
using ONEbook.UM.Helpers;
using ONEbook.UM.NETFramework;
using ONEbook.UM.Services;
using ONEbook.UM.Transport;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job
{
    public abstract class JobBase : IJob
    {
        protected JobBase()
        {
            var type = this.GetType();
            Logger = LoggerFactory.CreateLogger(type.FullName);
            SetUpUMEnviroment();
        }

        protected ILogWrapper Logger { get; set; }

        public abstract string ServiceName { get; }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                LogHelper.WriteInfo($"{ServiceName} - Start at {DateTime.Now}");

                var maintenanceService = new MaintenanceService(
                                      UMClientManager.RestClient,
                                      UMClientManager.CacheService,
                                      UMClientManager.UMClientConfigs,
                                      UMClientManager.HttpContext);

                var isUm = maintenanceService.IsUnderMaintenance();

                if (isUm)
                {
                    LogHelper.WriteInfo($"{ServiceName} - System is under maintained");
                }
                else
                {
                    await ImportDataToSystem(context);
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
        }

        protected abstract Task ImportDataToSystem(IJobExecutionContext context = null);

        private static void SetUpUMEnviroment()
        {
            var umConfig = new UMClientConfiguration().Initialize();

            UMClientManager
               .UseConfig(umConfig)
               .UseCacheService(new UMCacheService(new CacheService()))
               .UseRestClient(new UMRestClient(umConfig))
               .UseHttpContext(new EmptyHttpContextHelper());
        }
    }
}
