namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Affiliate.Link.Models;

    public class UnderMaintenanceService : IUnderMaintenanceService
    {
        public UnderMaintenanceTime GetUnderMaintenanceTime()
        {
            // Simplified implementation - always return not under maintenance
            return new UnderMaintenanceTime
            {
                IsUnderMaintenance = false,
                Message = "System is operational",
                StartTime = null,
                EndTime = null
            };
        }

        public bool IsUnderMaintenance()
        {
            return false; // Simplified - always return false
        }
    }
}