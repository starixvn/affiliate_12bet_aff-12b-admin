using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    public class ModuleManagementViewModel
    {
        public IEnumerable<ModuleTypeInfo> ModuleTypes { get; set; }

        public IEnumerable<NavigationTargetInfo> NavigationTargets { get; set; }

        public IEnumerable<LandingPageModule> ExistingModules { get; set; }

        public int TotalModules { get; set; }

        public bool CanCreate { get; set; } = true;

        public bool CanEdit { get; set; } = true;

        public bool CanDelete { get; set; } = true;
    }
}
