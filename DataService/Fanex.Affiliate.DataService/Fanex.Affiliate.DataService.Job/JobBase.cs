using Fanex.Affiliate.DataService.Job.Log;
using ONEbook.UM.NonWebApp;
using Quartz;
using System;
using System.Text;
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
                var isUm = UMDefaultInstances.MaintenanceService.IsUnderMaintenance();
                if (isUm)
                {
                    Logger.Info($"{ServiceName} - System is under maintained");
                }
                else
                {
                    await ImportDataToSystem(context);
                }

            }
            catch (System.Exception ex)
            {
                var builder = new StringBuilder();
                AddExceptionInfo(builder, ex, 3);
                Logger.Error(builder.ToString(), ex);
            }
        }

        protected abstract Task ImportDataToSystem(IJobExecutionContext context = null);

        private void AddExceptionInfo(StringBuilder builder, Exception exception,
                  int limitedInnerException)
        {
            builder.AppendLine();
            builder.AppendLine($"{ServiceName} - EXCEPTION INFO");
            builder.AppendLine($"Message: {exception.Message}");
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
