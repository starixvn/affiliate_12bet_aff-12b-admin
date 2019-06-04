using System;
using System.Configuration;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class PlaytechCasinoJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var dataMap = context.JobDetail.JobDataMap;
                var importService = (IDataImportingRunner)dataMap.Get("dataImportingRunner");

                var _umBusiness = (UMBusiness)dataMap.Get("umBusiness");
                var umUrl = ConfigurationManager.AppSettings["LBUrl"];
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);
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
