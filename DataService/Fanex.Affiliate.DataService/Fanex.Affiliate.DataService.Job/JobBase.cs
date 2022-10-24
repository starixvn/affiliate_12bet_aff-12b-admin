using Fanex.Affiliate.DataService.Job.Log;
using ONEbook.UM;
using ONEbook.UM.Services;
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
        }

        protected ILogWrapper Logger { get; set; }

        public abstract string ServiceName { get; }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Logger.Info($"{ServiceName} - Start at {DateTime.Now}");

                var maintenanceService = new MaintenanceService(
                                      UMClientManager.RestClient,
                                      UMClientManager.CacheService,
                                      UMClientManager.UMClientConfigs,
                                      UMClientManager.HttpContext);

                var isUm = maintenanceService.IsUnderMaintenance();

                if (isUm)
                {
                    Logger.Info($"{ServiceName} - System is under maintained");
                }
                else
                {
                    await ImportDataToSystem(context);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }


        protected abstract Task ImportDataToSystem(IJobExecutionContext context = null);
    }
}
