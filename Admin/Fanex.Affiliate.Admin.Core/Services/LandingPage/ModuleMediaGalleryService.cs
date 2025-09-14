using Fanex.Affiliate.Admin.Core.Commands.LandingPage;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Utilities.MediaGallery;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class ModuleMediaGalleryService : BaseDynamicService, IModuleMediaGalleryService
    {

        private readonly IAppSetting _appSetting;
        private readonly ISystemFile _systemFile;

        public ModuleMediaGalleryService(
            IDynamicRepository dynamicRepository,
            IAppSetting appSetting,
            ISystemFile systemFile) : base(dynamicRepository)
        {
            _appSetting = appSetting;
            _systemFile = systemFile;
        }

        public EntityResult<ModuleMedia> GetModuleMediaList(ModuleMediaParam param)
        {
            var criteria = new ModuleMediaCriteria
            {
                ModuleId = param.ModuleId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                SearchTerm = param.SearchTerm,
                FileType = param.FileType,
                SizeFilter = param.SizeFilter,
                IncludeDeleted = param.IncludeDeleted
            };

            return FetchEntityResult<ModuleMedia>(criteria);
        }

        public ModuleMedia GetMediaById(int mediaId)
        {
            var media = GetTData<ModuleMedia>(new ModuleMediaCriteria { MediaId = mediaId });
            if (media != null)
            {
                media.MediaRootUrl = _appSetting.BannerRootUrl;
            }
            return media;
        }

        public IEnumerable<ModuleMedia> GetMediaByModuleId(int moduleId)
        {
            var mediaList = FetchTData<ModuleMedia>(new ModuleMediaCriteria { ModuleId = moduleId });
            foreach (var media in mediaList)
            {
                media.MediaRootUrl = _appSetting.BannerRootUrl;
            }
            return mediaList;
        }

        public ResultInfo UploadModuleMedia(HttpPostedFileBase[] files, UploadModuleMediaParam param)
        {
            var results = new List<ResultInfo>();
            var successCount = 0;

            // Get module info to determine specific validation rules
            var moduleInfo = GetModuleInfo(param.ModuleId);
            if (moduleInfo == null)
            {
                return ResultInfo.GenerateErr("Invalid module ID");
            }

            foreach (var file in files)
            {
                try
                {
                    // Validate file based on module type and requirements
                    var validationResult = ValidateModuleMediaFile(file, moduleInfo);
                    if (!validationResult.IsSuccessful)
                    {
                        results.Add(validationResult);
                        continue;
                    }

                    var fileName = GenerateMediaFileName(file.FileName, param.ModuleId);
                    
                    // Create module-specific subfolder structure
                    var subFolder = GetModuleSubFolder(moduleInfo);
                    var relativePath = Path.Combine(subFolder, fileName);

                    // Ensure folder exists in all banner paths
                    var folderResult = CreateModuleFolder(subFolder);
                    if (!folderResult.IsSuccessful)
                    {
                        results.Add(folderResult);
                        continue;
                    }

                    // Check if file already exists (unless overwrite is allowed)
                    if (!param.OverwriteExisting)
                    {
                        var existsResult = CheckFileExists(relativePath);
                        if (!existsResult.IsSuccessful)
                        {
                            results.Add(existsResult);
                            continue;
                        }
                    }

                    // Optimize image if it's an image file and module type is image
                    OptimizedImageResult optimizedResult = null;
                    if (moduleInfo.ModuleType == 1 && IsImageFile(fileName)) // Type 1 = Image modules
                    {
                        optimizedResult = OptimizeImage(file, GetMaxWidthForModule(moduleInfo), GetMaxHeightForModule(moduleInfo));
                    }

                    // Save to all banner folders
                    if (optimizedResult?.WasOptimized == true)
                    {
                        SaveOptimizedFile(optimizedResult.OptimizedFilePath, relativePath);
                        fileName = optimizedResult.OptimizedFileName ?? fileName;
                        relativePath = Path.Combine(subFolder, fileName);
                    }
                    else
                    {
                        SaveUploadedFile(file, relativePath);
                    }

                    // Save to database
                    var command = new InsertModuleMediaCommand
                    {
                        ModuleId = param.ModuleId,
                        MediaName = param.MediaName ?? Path.GetFileNameWithoutExtension(file.FileName),
                        FileName = relativePath.Replace('\\', '/'), // Store with forward slashes for web URLs
                        Size = optimizedResult?.WasOptimized == true ? 
                            $"{optimizedResult.OptimizedWidth}x{optimizedResult.OptimizedHeight}" : 
                            GetImageDimensions(file),
                        UserId = param.UserId,
                        Ip = param.Ip
                    };

                    var dbResult = ExecuteReturnResultInfo(command);
                    if (dbResult.IsSuccessful)
                    {
                        successCount++;
                        
                        // Generate thumbnail for image modules
                        if (moduleInfo.ModuleType == 1 && IsImageFile(fileName))
                        {
                            GenerateThumbnail(relativePath, fileName);
                        }
                    }
                    results.Add(dbResult);
                }
                catch (Exception ex)
                {
                    results.Add(ResultInfo.GenerateErr($"Failed to upload {file.FileName}: {ex.Message}"));
                }
            }

            return successCount > 0
                ? new ResultInfo(0, $"Successfully uploaded {successCount} of {files.Length} files")
                : ResultInfo.GenerateErr("Failed to upload any files");
        }

        public ResultInfo UpdateModuleMedia(HttpPostedFileBase file, UpdateModuleMediaParam param)
        {
            var command = new UpdateModuleMediaCommand
            {
                MediaId = param.MediaId,
                MediaName = param.MediaName,
                Size = param.Size,
                UserId = param.UserId,
                Ip = param.Ip
            };

            if (file != null)
            {
                // Get existing media info to determine module
                var existingMedia = GetMediaById(param.MediaId);
                if (existingMedia == null)
                {
                    return ResultInfo.GenerateErr("Media not found");
                }

                var moduleInfo = GetModuleInfo(existingMedia.ModuleId);
                var validationResult = ValidateModuleMediaFile(file, moduleInfo);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult;
                }

                var fileName = GenerateMediaFileName(file.FileName, existingMedia.ModuleId);
                var subFolder = GetModuleSubFolder(moduleInfo);
                var relativePath = Path.Combine(subFolder, fileName);

                try
                {
                    // Delete old file if it exists
                    if (!string.IsNullOrEmpty(existingMedia.FileName))
                    {
                        DeleteMediaFile(existingMedia.FileName);
                    }

                    // Save new file to all banner folders
                    SaveUploadedFile(file, relativePath);
                    
                    command.FileName = relativePath.Replace('\\', '/');
                    command.Size = param.Size ?? GetImageDimensions(file);

                    // Generate new thumbnail for image files
                    if (moduleInfo?.ModuleType == 1 && IsImageFile(fileName))
                    {
                        GenerateThumbnail(relativePath, fileName);
                    }
                }
                catch (Exception ex)
                {
                    return ResultInfo.GenerateErr($"Failed to save file: {ex.Message}");
                }
            }

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteModuleMedia(DeleteModuleMediaParam param)
        {
            if (param.PermanentDelete)
            {
                // Delete physical files when permanent delete is requested
                var mediaIds = param.MediaIds.Split(',');
                foreach (var mediaIdStr in mediaIds)
                {
                    if (int.TryParse(mediaIdStr.Trim(), out int mediaId))
                    {
                        var media = GetMediaById(mediaId);
                        if (media != null && !string.IsNullOrEmpty(media.FileName))
                        {
                            try
                            {
                                DeleteMediaFile(media.FileName);
                                
                                // Also delete thumbnail if it exists
                                var thumbnailPath = GetThumbnailPath(media.FileName);
                                if (!string.IsNullOrEmpty(thumbnailPath))
                                {
                                    DeleteMediaFile(thumbnailPath);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteError(ex);
                                // Continue with database deletion even if file deletion fails
                            }
                        }
                    }
                }
            }

            var command = new DeleteModuleMediaCommand
            {
                MediaIds = param.MediaIds,
                PermanentDelete = param.PermanentDelete,
                DeleteReason = param.DeleteReason,
                UserId = param.UserId,
                Ip = param.Ip
            };
            return ExecuteReturnResultInfo(command);
        }

        public bool ValidateMediaFile(HttpPostedFileBase file, int moduleType)
        {
            var moduleInfo = GetModuleInfo(0); // Generic validation
            var result = ValidateModuleMediaFile(file, moduleInfo);
            return result.IsSuccessful;
        }

        private ResultInfo ValidateModuleMediaFile(HttpPostedFileBase file, dynamic moduleInfo)
        {
            if (file == null || file.ContentLength == 0)
            {
                return ResultInfo.GenerateErr(LandingPageConstants.ErrorMessages.NoFileProvided);
            }

            // Check file size
            if (file.ContentLength > LandingPageConstants.FileUpload.MaxFileSizeBytes)
            {
                return ResultInfo.GenerateErr($"{LandingPageConstants.ErrorMessages.FileSizeExceeded} ({FormatFileSize(file.ContentLength)}) exceeds maximum limit of {FormatFileSize(LandingPageConstants.FileUpload.MaxFileSizeBytes)}");
            }

            // Use BannerHelper for extension validation
            var extension = _systemFile.GetExtension(file.FileName);
            if (!Utilities.MediaGallery.BannerHelper.IsValidExtension(extension))
            {
                return ResultInfo.GenerateErr($"{LandingPageConstants.ErrorMessages.InvalidFileType} {extension}. Allowed types: {string.Join(", ", LandingPageConstants.FileExtensions.AllowedImageExtensions)}");
            }

            // Module type specific validation
            if (moduleInfo != null)
            {
                var moduleType = (int)moduleInfo.ModuleType;
                
                switch (moduleType)
                {
                    case LandingPageConstants.ModuleTypes.Image:
                        return ValidateImageFile(file, moduleInfo);
                    case LandingPageConstants.ModuleTypes.Contact:
                        return ValidateContactFile(file, moduleInfo);
                    case LandingPageConstants.ModuleTypes.Form:
                        return ValidateFormFile(file, moduleInfo);
                    case LandingPageConstants.ModuleTypes.Link:
                        return ResultInfo.GenerateErr(LandingPageConstants.ErrorMessages.LinkModuleNoUpload);
                    default:
                        return ValidateImageFile(file, moduleInfo); // Default to image validation
                }
            }

            return new ResultInfo(); // Valid
        }

        private ResultInfo ValidateImageFile(HttpPostedFileBase file, dynamic moduleInfo)
        {
            var extension = _systemFile.GetExtension(file.FileName);
            if (!IsImageFile(extension))
            {
                return ResultInfo.GenerateErr("Only image files are allowed for this module type");
            }

            try
            {
                using (var image = Image.FromStream(file.InputStream))
                {
                    var maxWidth = GetMaxWidthForModule(moduleInfo);
                    var maxHeight = GetMaxHeightForModule(moduleInfo);
                    
                    if (image.Width > maxWidth || image.Height > maxHeight)
                    {
                        return ResultInfo.GenerateErr($"{LandingPageConstants.ErrorMessages.ImageDimensionsExceeded} ({image.Width}x{image.Height}) exceed maximum allowed size ({maxWidth}x{maxHeight})");
                    }
                }
                // Reset stream position
                file.InputStream.Position = 0;
            }
            catch
            {
                return ResultInfo.GenerateErr(LandingPageConstants.ErrorMessages.InvalidImageData);
            }

            return new ResultInfo(); // Valid
        }

        private ResultInfo ValidateContactFile(HttpPostedFileBase file, dynamic moduleInfo)
        {
            // Contact modules might allow images or documents
            var extension = _systemFile.GetExtension(file.FileName);
            if (IsImageFile(extension))
            {
                return ValidateImageFile(file, moduleInfo);
            }
            
            // Could add document validation here if needed
            return new ResultInfo(); // Valid
        }

        private ResultInfo ValidateFormFile(HttpPostedFileBase file, dynamic moduleInfo)
        {
            // Form modules might allow images or documents
            var extension = _systemFile.GetExtension(file.FileName);
            if (IsImageFile(extension))
            {
                return ValidateImageFile(file, moduleInfo);
            }
            
            // Could add document validation here if needed
            return new ResultInfo(); // Valid
        }

        public string GenerateMediaFileName(string originalFileName, int moduleId)
        {
            var extension = Path.GetExtension(originalFileName);
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var guid = Guid.NewGuid().ToString("N").Substring(0, 8);
            return $"Module_{moduleId}_{timestamp}_{guid}{extension}";
        }

        public MediaStatistics GetModuleMediaStatistics(int moduleId)
        {
            var criteria = new ModuleMediaStatisticsCriteria { ModuleId = moduleId };
            return GetTData<MediaStatistics>(criteria);
        }

        public OptimizedImageResult OptimizeImage(HttpPostedFileBase file, int maxWidth, int maxHeight)
        {
            var result = new OptimizedImageResult
            {
                OriginalFileName = file.FileName,
                OriginalSizeBytes = file.ContentLength,
                WasOptimized = false
            };

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (!LandingPageConstants.FileExtensions.AllImageExtensions.Contains(extension))
            {
                result.OptimizationMessage = "File is not an image, skipping optimization";
                return result;
            }

            try
            {
                using (var image = Image.FromStream(file.InputStream))
                {
                    result.OriginalWidth = image.Width;
                    result.OriginalHeight = image.Height;

                    // Check if optimization is needed
                    if (image.Width <= maxWidth && image.Height <= maxHeight)
                    {
                        result.OptimizedWidth = image.Width;
                        result.OptimizedHeight = image.Height;
                        result.OptimizationMessage = "Image already within size limits";
                        return result;
                    }

                    // Calculate new dimensions
                    var ratioX = (double)maxWidth / image.Width;
                    var ratioY = (double)maxHeight / image.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var newWidth = (int)(image.Width * ratio);
                    var newHeight = (int)(image.Height * ratio);

                    result.OptimizedWidth = newWidth;
                    result.OptimizedHeight = newHeight;

                    // Create optimized image
                    using (var resized = new Bitmap(newWidth, newHeight))
                    {
                        using (var graphics = Graphics.FromImage(resized))
                        {
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                        }

                        // Save to temp file
                        var tempPath = Path.GetTempFileName();
                        var format = GetImageFormat(extension);
                        resized.Save(tempPath, format);

                        var optimizedInfo = new FileInfo(tempPath);
                        result.OptimizedSizeBytes = optimizedInfo.Length;
                        result.OptimizedFilePath = tempPath;
                        result.OptimizedFileName = GenerateMediaFileName(file.FileName, 0);
                        result.WasOptimized = true;
                        result.SizeSavedBytes = result.OriginalSizeBytes - result.OptimizedSizeBytes;
                        result.CompressionRatio = (double)result.OptimizedSizeBytes / result.OriginalSizeBytes;
                        result.OptimizationMessage = $"Image resized from {image.Width}x{image.Height} to {newWidth}x{newHeight}";
                    }
                }
            }
            catch (Exception ex)
            {
                result.OptimizationMessage = $"Optimization failed: {ex.Message}";
            }

            return result;
        }

        private string GetImageDimensions(HttpPostedFileBase file)
        {
            try
            {
                var dimensions = ImageDimensionHelper.ExtractImageDimensions(file.InputStream);
                return dimensions == "0x0" ? "Unknown" : dimensions;
            }
            catch
            {
                return "Unknown";
            }
            finally
            {
                file.InputStream.Position = 0;
            }
        }

        private static ImageFormat GetImageFormat(string extension)
        {
            switch (extension != null ? extension.ToLowerInvariant() : string.Empty)
            {
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".gif":
                    return ImageFormat.Gif;
                case ".bmp":
                    return ImageFormat.Bmp;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        // New helper methods for proper file storage using BannerFolders

        private dynamic GetModuleInfo(int moduleId)
        {
            // Get module information from database to determine validation rules
            var criteria = new Criterias.LandingPage.LandingPageModuleCriteria { ModuleId = moduleId };
            return GetTData<DTO.LandingPage.LandingPageModule>(criteria);
        }

        private string GetModuleSubFolder(dynamic moduleInfo)
        {
            if (moduleInfo == null) 
            {
                return Path.Combine(LandingPageConstants.FolderStructure.LandingPageRoot, LandingPageConstants.FolderStructure.GeneralFolder);
            }

            var moduleType = (int)moduleInfo.ModuleType;
            var moduleName = ((string)moduleInfo.ModuleName ?? "Unknown").Replace(" ", "");

            switch (moduleType)
            {
                case LandingPageConstants.ModuleTypes.Image:
                    return Path.Combine(LandingPageConstants.FolderStructure.LandingPageRoot, LandingPageConstants.FolderStructure.ImagesFolder, moduleName);
                case LandingPageConstants.ModuleTypes.Contact:
                    return Path.Combine(LandingPageConstants.FolderStructure.LandingPageRoot, LandingPageConstants.FolderStructure.ContactFolder, moduleName);
                case LandingPageConstants.ModuleTypes.Form:
                    return Path.Combine(LandingPageConstants.FolderStructure.LandingPageRoot, LandingPageConstants.FolderStructure.FormsFolder, moduleName);
                case LandingPageConstants.ModuleTypes.Link:
                    return Path.Combine(LandingPageConstants.FolderStructure.LandingPageRoot, LandingPageConstants.FolderStructure.LinksFolder, moduleName);
                default:
                    return Path.Combine(LandingPageConstants.FolderStructure.LandingPageRoot, $"Module_{moduleInfo.ModuleId}");
            }
        }

        private int GetMaxWidthForModule(dynamic moduleInfo)
        {
            if (moduleInfo == null) 
            {
                return LandingPageConstants.FileUpload.MaxImageWidth;
            }

            var moduleName = (string)moduleInfo.ModuleName ?? "";
            
            // Define specific size limits based on module purpose
            switch (moduleName.ToLowerInvariant())
            {
                case LandingPageConstants.ModuleNames.Background:
                    return LandingPageConstants.ModuleImageLimits.BackgroundMaxWidth;
                case LandingPageConstants.ModuleNames.HeroImage:
                    return LandingPageConstants.ModuleImageLimits.HeroMaxWidth;
                case LandingPageConstants.ModuleNames.LogoSponsor1:
                case LandingPageConstants.ModuleNames.LogoSponsor2:
                    return LandingPageConstants.ModuleImageLimits.LogoMaxWidth;
                case LandingPageConstants.ModuleNames.CTAButton:
                    return LandingPageConstants.ModuleImageLimits.ButtonMaxWidth;
                default:
                    return LandingPageConstants.FileUpload.MaxImageWidth;
            }
        }

        private int GetMaxHeightForModule(dynamic moduleInfo)
        {
            if (moduleInfo == null) 
            {
                return LandingPageConstants.FileUpload.MaxImageHeight;
            }

            var moduleName = (string)moduleInfo.ModuleName ?? "";
            
            switch (moduleName.ToLowerInvariant())
            {
                case LandingPageConstants.ModuleNames.Background:
                    return LandingPageConstants.ModuleImageLimits.BackgroundMaxHeight;
                case LandingPageConstants.ModuleNames.HeroImage:
                    return LandingPageConstants.ModuleImageLimits.HeroMaxHeight;
                case LandingPageConstants.ModuleNames.LogoSponsor1:
                case LandingPageConstants.ModuleNames.LogoSponsor2:
                    return LandingPageConstants.ModuleImageLimits.LogoMaxHeight;
                case LandingPageConstants.ModuleNames.CTAButton:
                    return LandingPageConstants.ModuleImageLimits.ButtonMaxHeight;
                default:
                    return LandingPageConstants.FileUpload.MaxImageHeight;
            }
        }

        private ResultInfo CreateModuleFolder(string subFolder)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var result = _systemFile.CreateFolder(serverPath, subFolder);
                if (!result.IsSuccessful)
                {
                    return result;
                }
            }
            return new ResultInfo();
        }

        private ResultInfo CheckFileExists(string relativePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var result = _systemFile.CheckExistedFile(serverPath, relativePath);
                if (!result.IsSuccessful)
                {
                    return result;
                }
            }
            return new ResultInfo();
        }

        private void SaveUploadedFile(HttpPostedFileBase file, string relativePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(serverPath, relativePath);
                _systemFile.SaveFile(file, fullPath);
            }
        }

        private void SaveOptimizedFile(string tempFilePath, string relativePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(serverPath, relativePath);
                _systemFile.CopyFile(tempFilePath, fullPath, true);
            }
        }

        private void DeleteMediaFile(string relativePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.DeleteFile(serverPath, relativePath);
            }
        }

        private bool IsImageFile(string fileName)
        {
            var extension = _systemFile.GetExtension(fileName);
            return Utilities.MediaGallery.BannerHelper.IsValidExtension(extension);
        }

        private void GenerateThumbnail(string relativePath, string fileName)
        {
            try
            {
                var thumbnailFolder = Path.Combine(Path.GetDirectoryName(relativePath), LandingPageConstants.FolderStructure.ThumbnailsFolder);
                var thumbnailFileName = Path.GetFileNameWithoutExtension(fileName) + "_thumb" + Path.GetExtension(fileName);
                var thumbnailPath = Path.Combine(thumbnailFolder, thumbnailFileName);

                // Create thumbnail folders
                CreateModuleFolder(thumbnailFolder);

                // Generate thumbnail for each server path
                foreach (var serverPath in _appSetting.BannerFolders)
                {
                    var sourceFile = Path.Combine(serverPath, relativePath);
                    var thumbFile = Path.Combine(serverPath, thumbnailPath);

                    if (_systemFile.FileExists(sourceFile))
                    {
                        using (var original = Image.FromFile(sourceFile))
                        {
                            using (var thumbnail = new Bitmap(LandingPageConstants.FileUpload.ThumbnailSize, LandingPageConstants.FileUpload.ThumbnailSize))
                            {
                                using (var graphics = Graphics.FromImage(thumbnail))
                                {
                                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                    graphics.DrawImage(original, 0, 0, LandingPageConstants.FileUpload.ThumbnailSize, LandingPageConstants.FileUpload.ThumbnailSize);
                                }

                                _systemFile.UploadFile(thumbnail, thumbFile);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log thumbnail generation error but don't fail the upload
                LogHelper.WriteError(ex);
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB" };
            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number / LandingPageConstants.FileUpload.BytesPerKilobyte) >= 1)
            {
                number = number / LandingPageConstants.FileUpload.BytesPerKilobyte;
                counter++;
            }
            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }

        private string GetThumbnailPath(string originalPath)
        {
            if (string.IsNullOrEmpty(originalPath))
            {
                return null;
            }

            var directory = Path.GetDirectoryName(originalPath);
            var fileName = Path.GetFileNameWithoutExtension(originalPath);
            var extension = Path.GetExtension(originalPath);
            
            return Path.Combine(directory, LandingPageConstants.FolderStructure.ThumbnailsFolder, fileName + "_thumb" + extension).Replace('\\', '/');
        }

        public EntityResult<ModuleMedia> GetModuleMediaList(ModuleMediaCriteria criteria)
        {
            throw new NotImplementedException();
        }

        EntityResult<AssetInfo> IModuleMediaGalleryService.GetModuleMediaList(ModuleMediaCriteria criteria)
        {
            throw new NotImplementedException();
        }

        AssetInfo IModuleMediaGalleryService.GetMediaById(int mediaId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<AssetInfo> IModuleMediaGalleryService.GetMediaByModuleId(int moduleId)
        {
            throw new NotImplementedException();
        }
    }
}
