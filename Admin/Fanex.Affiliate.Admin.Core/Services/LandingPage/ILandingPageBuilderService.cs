using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Collections.Generic;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface ILandingPageBuilderService
    {
        #region Landing Page Management
        
        /// <summary>
        /// Get list of landing pages with filtering
        /// </summary>
        EntityResult<LandingPageTemplate> GetLandingPageList(LandingPageListParam param = null);

        /// <summary>
        /// Get landing page by ID
        /// </summary>
        LandingPageTemplate GetLandingPageById(int pageId);

        /// <summary>
        /// Delete landing page
        /// </summary>
        ResultInfo DeleteLandingPage(DeleteLandingPageParam param);

        /// <summary>
        /// Publish/unpublish landing page
        /// </summary>
        ResultInfo PublishLandingPage(PublishLandingPageParam param);

        /// <summary>
        /// Generate landing page preview
        /// </summary>
        LandingPagePreview GenerateLandingPagePreview(int pageId);

        #endregion

        #region Template Management

        /// <summary>
        /// Save landing page template configuration
        /// </summary>
        ResultInfo SaveLandingPageTemplate(SaveLandingPageTemplateParam param);

        /// <summary>
        /// Add module to landing page
        /// </summary>
        ResultInfo AddModuleToPage(AddModuleToPageParam param);

        /// <summary>
        /// Remove module from landing page
        /// </summary>
        ResultInfo RemoveModuleFromPage(RemoveModuleFromPageParam param);

        /// <summary>
        /// Reorder modules on landing page
        /// </summary>
        ResultInfo ReorderModules(ReorderModulesParam param);

        /// <summary>
        /// Update module configuration on page
        /// </summary>
        ResultInfo UpdateModuleConfiguration(UpdateModuleConfigurationParam param);

        /// <summary>
        /// Get live preview of page configuration
        /// </summary>
        LandingPagePreview GetLivePreview(GetLivePreviewParam param);

        #endregion

        #region Media Assignment

        /// <summary>
        /// Assign media to module in page builder
        /// </summary>
        ResultInfo AssignMediaToModule(AssignMediaToModuleParam param);

        /// <summary>
        /// Remove media assignment from module
        /// </summary>
        ResultInfo RemoveMediaFromModule(RemoveMediaFromModuleParam param);

        /// <summary>
        /// Get assigned media for a specific module
        /// </summary>
        EntityResult<AssetInfo> GetModuleAssignedMedia(int moduleId, int pageId);

        #endregion
    }
}
