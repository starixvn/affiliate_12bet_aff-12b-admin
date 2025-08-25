using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IModuleMediaGalleryService
    {
        /// <summary>
        /// Get paginated list of media files for a module
        /// </summary>
        /// <param name="param">Module media parameters including module ID and pagination</param>
        /// <returns>Paginated result of module media files</returns>
        EntityResult<AssetInfo> GetModuleMediaList(ModuleMediaCriteria criteria);

        /// <summary>
        /// Get media file by ID
        /// </summary>
        /// <param name="mediaId">Media file identifier</param>
        /// <returns>Media file details or null if not found</returns>
        AssetInfo GetMediaById(int mediaId);

        /// <summary>
        /// Get all media files for a specific module
        /// </summary>
        /// <param name="moduleId">Module identifier</param>
        /// <returns>List of media files for the module</returns>
        IEnumerable<AssetInfo> GetMediaByModuleId(int moduleId);

        /// <summary>
        /// Upload multiple media files for a module
        /// </summary>
        /// <param name="files">Array of uploaded files</param>
        /// <param name="param">Upload parameters</param>
        /// <returns>Upload operation result</returns>
        ResultInfo UploadModuleMedia(HttpPostedFileBase[] files, UploadModuleMediaParam param);

        /// <summary>
        /// Update existing media file
        /// </summary>
        /// <param name="file">New file (optional - for file replacement)</param>
        /// <param name="param">Update parameters</param>
        /// <returns>Update operation result</returns>
        ResultInfo UpdateModuleMedia(HttpPostedFileBase file, UpdateModuleMediaParam param);

        /// <summary>
        /// Delete media files
        /// </summary>
        /// <param name="param">Delete parameters with media IDs</param>
        /// <returns>Delete operation result</returns>
        ResultInfo DeleteModuleMedia(DeleteModuleMediaParam param);

        /// <summary>
        /// Validate uploaded media file
        /// </summary>
        /// <param name="file">File to validate</param>
        /// <param name="moduleType">Module type for type-specific validation</param>
        /// <returns>True if file is valid, false otherwise</returns>
        bool ValidateMediaFile(HttpPostedFileBase file, int moduleType);

        /// <summary>
        /// Generate unique file name for media file
        /// </summary>
        /// <param name="originalFileName">Original file name</param>
        /// <param name="moduleId">Module identifier</param>
        /// <returns>Generated unique file name</returns>
        string GenerateMediaFileName(string originalFileName, int moduleId);

        /// <summary>
        /// Get media file statistics for a module
        /// </summary>
        /// <param name="moduleId">Module identifier</param>
        /// <returns>Media statistics including file count and total size</returns>
        MediaStatistics GetModuleMediaStatistics(int moduleId);

        /// <summary>
        /// Optimize uploaded image file
        /// </summary>
        /// <param name="file">Image file to optimize</param>
        /// <param name="maxWidth">Maximum width</param>
        /// <param name="maxHeight">Maximum height</param>
        /// <returns>Optimized file information</returns>
        OptimizedImageResult OptimizeImage(HttpPostedFileBase file, int maxWidth, int maxHeight);
    }
}
