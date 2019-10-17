using ONEbook.UM.Models;
using ONEbook.UM.Services.Impl;

namespace Fanex.Affiliate.Admin.Core.Services.UnderMaintenanceService
{
    public class UnderMaintenanceService: IUnderMaintenanceService
    {
        private readonly MaintenanceService _maintenaceService;
        public UnderMaintenanceService()
        {
            _maintenaceService = new MaintenanceService();
        }

        public UnderMaintenanceTime GetUnderMaintenanceTime()
        {
            return _maintenaceService.GetUnderMaintenanceTime();
        }

        public bool IsUnderMaintenance()
        {
            return _maintenaceService.IsUnderMaintenance();
        }
    }
}
