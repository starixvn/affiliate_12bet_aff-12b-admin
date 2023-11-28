namespace Fanex.Affiliate.Link.Services.Interface
{
    using ONEbook.UM.Models;

    public interface IUnderMaintenanceService
    {
        UnderMaintenanceTime GetUnderMaintenanceTime();

        bool IsUnderMaintenance();
    }
}
