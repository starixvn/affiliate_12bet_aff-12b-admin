using System;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Logging;
using Quartz;
using System.Threading.Tasks;
using ONEbook.UM.NonWebApp;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class PlaytechCasinoJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dataMap = context.JobDetail.JobDataMap;
                var importService = (IDataImportingRunner)dataMap.Get("dataImportingRunner");

                var isUm = UMDefaultInstances.MaintenanceService.IsUnderMaintenance();
                if (!isUm)
                {
                    var importResult = importService.Run();
                    if (!importResult.IsSuccessful)
                    {
                        LogService.Error($"{importResult.Code}\n{importResult.Message}");
                    }
                }
                else
                {
                    LogService.Info("12Bet Aff service was suspended by UM");
                }
            }
            catch (Exception exception)
            {
                LogService.Error(GetExceptionMessage(exception), exception.InnerException);
            }
            return Task.CompletedTask;
        }

        private static string GetExceptionMessage(Exception exception)
        {
            var messages = new string[]
            {
                "ERROR: " + exception.Message,
                "SOURCE: " + exception.Source,
                "TARGETSITE: " + exception.TargetSite,
                "STACKTRACE: " + exception.StackTrace
            };

            return string.Join(Environment.NewLine, messages);
        }
    }
}
