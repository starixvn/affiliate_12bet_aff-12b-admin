using ONEbook.UM.Models;
using ONEbook.UM.Services;

namespace Fanex.Affiliate.Admin.Core.Services.UnderMaintenanceService
{
    public class UnderMaintenanceService : IUnderMaintenanceService
    {
        private readonly IMaintenanceService _maintenaceService;
        public UnderMaintenanceService(IMaintenanceService maintenaceService)
        {
            _maintenaceService = maintenaceService;
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
