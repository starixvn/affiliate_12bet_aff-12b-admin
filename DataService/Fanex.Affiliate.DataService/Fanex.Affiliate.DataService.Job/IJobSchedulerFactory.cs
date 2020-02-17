using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job
{
    public interface IJobSchedulerFactory
    {
        Task SetUpAsync();

        Task ShutdownAsync();

        Task PauseAsync();

        Task ResumeAsync();
    }
}
