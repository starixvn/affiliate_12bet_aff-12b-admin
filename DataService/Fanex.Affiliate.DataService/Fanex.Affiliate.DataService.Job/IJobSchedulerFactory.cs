namespace Fanex.Affiliate.DataService.Job
{
    public interface IJobSchedulerFactory
    {
        void SetUp();

        void Shutdown();

        void Pause();

        void Resume();
    }
}
