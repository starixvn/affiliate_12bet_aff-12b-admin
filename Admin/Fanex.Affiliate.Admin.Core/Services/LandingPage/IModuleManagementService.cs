using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IModuleManagementService
    {
        /// <summary>
        /// Get all available modules
        /// </summary>
        /// <returns>List of landing page modules</returns>
        IEnumerable<LandingPageModule> GetModuleList();

        /// <summary>
        /// Get module by ID
        /// </summary>
        /// <param name="moduleId">Module identifier</param>
        /// <returns>Module details or null if not found</returns>
        LandingPageModule GetModuleById(int moduleId);

        /// <summary>
        /// Get modules filtered by type
        /// </summary>
        /// <param name="moduleType">Module type (1-4)</param>
        /// <returns>List of modules of specified type</returns>
        IEnumerable<LandingPageModule> GetModulesByType(int moduleType);

        /// <summary>
        /// Create a new module
        /// </summary>
        /// <param name="param">Module creation parameters</param>
        /// <returns>Operation result</returns>
        ResultInfo InsertModule(InsertUpdateModuleParam param);

        /// <summary>
        /// Update an existing module
        /// </summary>
        /// <param name="param">Module update parameters</param>
        /// <returns>Operation result</returns>
        ResultInfo UpdateModule(InsertUpdateModuleParam param);

        /// <summary>
        /// Delete modules
        /// </summary>
        /// <param name="param">Delete parameters with module IDs</param>
        /// <returns>Operation result</returns>
        ResultInfo DeleteModule(DeleteModuleParam param);

        /// <summary>
        /// Get available module types
        /// </summary>
        /// <returns>List of module type information</returns>
        IEnumerable<ModuleTypeInfo> GetModuleTypes();

        /// <summary>
        /// Get available navigation targets
        /// </summary>
        /// <returns>List of navigation target information</returns>
        IEnumerable<NavigationTargetInfo> GetNavigationTargets();

        /// <summary>
        /// Validate if module name is unique
        /// </summary>
        /// <param name="moduleName">Module name to check</param>
        /// <param name="moduleId">Exclude this module ID from check (for updates)</param>
        /// <returns>True if unique, false if duplicate</returns>
        bool ValidateModuleUniqueness(string moduleName, int moduleId = 0);
    }
}
