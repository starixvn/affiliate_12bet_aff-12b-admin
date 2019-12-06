using Fanex.Logging;
using ONEbook.UM.NonWebApp;
using Quartz;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job
{
    public abstract class JobBase : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var isUm = UMDefaultInstances.MaintenanceService.IsUnderMaintenance();
                if (isUm)
                {
                    LogService.Info("System is under maintained");
                }
                else
                {
                    await ImportDataToSystem();
                }

            }
            catch (System.Exception ex)
            {
                var builder = new StringBuilder();
                AddExceptionInfo(builder, ex, 5);
                LogService.Error(builder.ToString(), ex);
            }

        }

        protected abstract Task ImportDataToSystem(IJobExecutionContext context = null);

        private void AddExceptionInfo(StringBuilder builder, Exception exception,
                  int limitedInnerException)
        {
            builder.AppendLine();
            builder.AppendLine("EXCEPTION INFO");
            builder.AppendLine($"Source: {exception.Source}");
            builder.AppendLine($"TargetSite: {exception.TargetSite}");
            builder.AppendLine($"StackTrace: {exception.StackTrace}");
            if (exception.InnerException != null && limitedInnerException > 0)
            {
                limitedInnerException--;
                builder.AppendLine("INNER EXCEPTION");
                AddExceptionInfo(builder, exception.InnerException, limitedInnerException);
            }
        }
    }
}
