using Fanex.Affiliate.Admin.Core.Commands.LandingPage;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class AssetUploaderService : BaseDynamicService, IAssetUploaderService
    {
        private const int MaxLength = 50;
        private const int DefaultCompressionPercentage = 25;
        private const string RestrictedMultiInputFileNamesPattern = "[\\\\/:*?\"<>#%&+]";

        private readonly IAppSetting _appSetting;
        private readonly ISystemFile _systemFile;
        private readonly ILandingPageService _landingPageService;

        public AssetUploaderService(
            IDynamicRepository dynamicRepository,
            IAppSetting appSetting,
            ISystemFile systemFile,
            ILandingPageService landingPageService) : base(dynamicRepository)
        {
            _appSetting = appSetting;
            _systemFile = systemFile;
            _landingPageService = landingPageService;
        }

        public ResultInfo UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            // Validate all files first
            var result = ValidateAllFiles(files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            // Validate input file names
            if (RegexHelper.IsMatch(param.InputFileNames, RestrictedMultiInputFileNamesPattern))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = Lang.FileNameCanNotContain
                };
            }

            var inputFileNames = string.IsNullOrEmpty(param.InputFileNames) 
                ? files.Select(f => f.FileName).ToArray()
                : param.InputFileNames.Split('|');

            result = ValidateAllInputFileNames(inputFileNames, files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            // Check for duplicate files in target folder
            var folder = GetAssetFolder(param.ModuleId, param.TargetCountryCode, param.MarketId);
            result = CheckMultipleFilesInFolder(inputFileNames, folder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            // Process and upload files
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var inputFileName = inputFileNames[i];
                var path = $"{folder}/{inputFileName}";

                var dimensions = ExtractImageDimensions(file.InputStream);
                param.MediaNames.Add(inputFileName);
                param.FilePaths.Add(path);
                param.Dimensions.Add(dimensions);

                // Upload file to all configured server paths
                UploadFile(file, path);
            }

            // Build files XML for database insertion
            BuildFilesXml(param);

            // Insert asset records to database
            try
            {
                result = InsertAssets(param);
            } catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                result = ResultInfo.GenerateErr("Database insertion fails");
            }

            // If database insertion fails, clean up uploaded files
            if (!result.IsSuccessful)
            {
                foreach (var path in param.FilePaths)
                {
                    DeleteFile(path);
                }
            }

            return result;
        }

        public ResultInfo UpdateAsset(HttpPostedFileBase file, UploadAssetsParam param)
        {
            var result = ValidationInputFileName(file, param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var oldAssetInfo = GetAssetDetail(new AssetListParam { MediaId = param.MediaId });
            if (oldAssetInfo == null)
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = "Asset not found."
                };
            }

            return ProcessAssetUpdate(file, param, oldAssetInfo);
        }

        private ResultInfo ProcessAssetUpdate(HttpPostedFileBase file, UploadAssetsParam param, LandingPageAsset oldAssetInfo)
        {
            var oldFilePath = oldAssetInfo.FileName;
            var folder = GetAssetFolder(param.ModuleId, param.TargetCountryCode, param.MarketId);
            var inputFileName = string.IsNullOrEmpty(param.InputFileNames) ? file?.FileName : param.InputFileNames;
            var path = $"{folder}/{inputFileName}";

            var result = CheckFileInFolderForUpdate(path, oldFilePath);
            if (!result.IsSuccessful)
            {
                return result;
            }

            param.FilePaths.Add(path);
            param.MediaNames.Add(inputFileName);

            return file != null 
                ? ProcessFileUpdate(file, param, path, oldFilePath)
                : ProcessMetadataUpdate(param, path, oldFilePath, oldAssetInfo);
        }

        private ResultInfo ProcessFileUpdate(HttpPostedFileBase file, UploadAssetsParam param, string path, string oldFilePath)
        {
            var result = ValidateFile(file.FileName, file.ContentLength, file.InputStream);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var dimensions = ExtractImageDimensions(file.InputStream);
            param.Dimensions.Add(dimensions);
            BuildFilesXml(param);
            result = ExecuteUpdateAsset(param);

            if (result.IsSuccessful)
            {
                DeleteFile(oldFilePath);
                UploadFile(file, path);
            }

            return result;
        }

        private ResultInfo ProcessMetadataUpdate(UploadAssetsParam param, string path, string oldFilePath, LandingPageAsset oldAssetInfo)
        {
            param.Dimensions.Add(oldAssetInfo.Dimension ?? "0x0");
            BuildFilesXml(param);
            var result = ExecuteUpdateAsset(param);

            if (result.IsSuccessful && !string.Equals(path, oldFilePath, StringComparison.CurrentCultureIgnoreCase))
            {
                MoveFile(path, oldFilePath);
            }

            return result;
        }

        private static string ExtractImageDimensions(Stream inputStream)
        {
            try
            {
                using (var bitmap = new Bitmap(inputStream))
                {
                    return $"{bitmap.Width}x{bitmap.Height}";
                }
            }
            catch
            {
                return "0x0";
            }
        }

        public EntityResult<LandingPageAsset> GetAssetList(AssetListParam param)
        {
            var criteria = param.MapTo<AssetListParam, GetAssetListCriteria>();

            return FetchEntityResult<LandingPageAsset>(criteria);
        }

        public LandingPageAsset GetAssetDetail(AssetListParam param)
        {
            var criteria = param.MapTo<AssetListParam, GetAssetListCriteria>();

            return GetTData<LandingPageAsset>(criteria);
        }

        public ResultInfo ToggleAssetStatus(UpdateMediasStatusParam param)
        {
            var command = param.MapTo<UpdateMediasStatusParam, UpdateMediasStatusCommand>();

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteMedias(DeleteMediasParam param)
        {
            return DeleteAssets(param);
        }

        public OptimizedAssetResult OptimizeAsset(OptimizeAssetParam param)
        {
            return new OptimizedAssetResult
            {
                WasOptimized = true,
                OriginalFileName = "sample.jpg",
                OptimizedFileName = "sample_optimized.jpg",
                CompressionPercentage = DefaultCompressionPercentage
            };
        }

        public AssetStatistics GetAssetStatistics(int? moduleId)
        {
            return new AssetStatistics
            {
                TotalAssets = 0,
                TotalSize = 0,
                ImageCount = 0,
                VideoCount = 0,
                DocumentCount = 0,
                TotalSizeFormatted = "0 KB"
            };
        }

        #region Private Helper Methods

        private ResultInfo ValidateAllFiles(HttpPostedFileBase[] files)
        {
            var resultValidateAllFiles = new ResultInfo();
            var message = new StringBuilder();
            
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var result = ValidateFile(fileName, file.ContentLength, file.InputStream);

                if (!result.IsSuccessful)
                {
                    resultValidateAllFiles.Code = result.Code;
                    message.Append($"File {fileName} is incorrect.<br />" + result.Message);
                }
            }

            resultValidateAllFiles.Message = message.ToString();
            return resultValidateAllFiles;
        }

        private ResultInfo ValidateFile(string fileName, int contentLength, Stream inputStream)
        {
            // Check file size
            if (contentLength > LandingPageConstants.FileUpload.MaxFileSizeBytes)
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = LandingPageConstants.ErrorMessages.FileSizeExceeded
                };
            }

            // Check file extension
            var extension = _systemFile.GetExtension(fileName).ToLower(System.Globalization.CultureInfo.InvariantCulture);
            if (!LandingPageConstants.FileExtensions.AllImageExtensions.Contains(extension))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = LandingPageConstants.ErrorMessages.InvalidFileType
                };
            }

            // Validate image dimensions if it's an image file
            try
            {
                using (var bitmap = new Bitmap(inputStream))
                {
                    if (bitmap.Width > LandingPageConstants.FileUpload.MaxImageWidth || 
                        bitmap.Height > LandingPageConstants.FileUpload.MaxImageHeight)
                    {
                        return new ResultInfo
                        {
                            Code = ErrCode.GeneralErrorCode,
                            Message = LandingPageConstants.ErrorMessages.ImageDimensionsExceeded
                        };
                    }
                }
            }
            catch
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = LandingPageConstants.ErrorMessages.InvalidImageData
                };
            }

            return new ResultInfo();
        }

        private ResultInfo CheckDuplicateFileNames(string[] inputFileNames)
        {
            var length = inputFileNames.Length;
            for (var i = 0; i <= length - 1; i++)
            {
                var fileName = inputFileNames[i].Trim();

                for (var j = i + 1; j < length; j++)
                {
                    if (string.Equals(fileName, inputFileNames[j].Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        return new ResultInfo
                        {
                            Code = ErrCode.GeneralErrorCode,
                            Message = "File Name is unique, please enter other file name!"
                        };
                    }
                }
            }

            return new ResultInfo();
        }

        private ResultInfo ValidateAllInputFileNames(string[] inputFileNames, HttpPostedFileBase[] files)
        {
            var result = CheckDuplicateFileNames(inputFileNames);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var message = new StringBuilder();
            for (int i = 0; i < inputFileNames.Length; i++)
            {
                var inputFileName = inputFileNames[i];

                if (string.IsNullOrEmpty(inputFileNames[i]))
                {
                    var file = files[i];
                    inputFileNames[i] = file.FileName;

                    if (file.FileName.Length > MaxLength)
                    {
                        result.Code = ErrCode.GeneralErrorCode;
                        message.Append($"The maximum length of file name is 50. Please choose another name for File Name {i + 1} or select another File.<br />");
                    }
                }
                else
                {
                    var extension = _systemFile.GetExtension(inputFileName).ToLower(System.Globalization.CultureInfo.InvariantCulture);
                    if (!LandingPageConstants.FileExtensions.AllImageExtensions.Contains(extension))
                    {
                        result.Code = ErrCode.GeneralErrorCode;
                        message.Append($"File Name {i + 1} is incorrect.<br />" + LandingPageConstants.ErrorMessages.InvalidFileType);
                    }

                    if (inputFileName.Length > MaxLength)
                    {
                        result.Code = ErrCode.GeneralErrorCode;
                        message.Append($"The maximum length of file name is 50. Please choose another name File Name {i + 1}.<br />");
                    }
                }
            }

            result.Message = message.ToString();
            return result;
        }

        private ResultInfo CheckMultipleFilesInFolder(string[] inputFileNames, string folder)
        {
            var resultCheckMultipleFilesInFolder = new ResultInfo();
            var message = new StringBuilder();

            for (int i = 0; i < inputFileNames.Length; i++)
            {
                var inputFileName = inputFileNames[i];
                var filePath = $"{folder}/{inputFileName}";
                var result = CheckFileInFolder(filePath);

                if (!result.IsSuccessful)
                {
                    if (result.Code == ErrCode.ErrCanNotAccessFolder)
                    {
                        return result;
                    }

                    resultCheckMultipleFilesInFolder.Code = ErrCode.GeneralErrorCode;
                    message.Append($"{inputFileName}, ");
                }
            }

            resultCheckMultipleFilesInFolder.Message = resultCheckMultipleFilesInFolder.IsSuccessful ? string.Empty
                : message.Append("This file name(s) belong to existing asset(s). Please change the file name.").ToString();

            return resultCheckMultipleFilesInFolder;
        }

        private ResultInfo CheckFileInFolder(string filePath)
        {
            // Check if file already exists in any of the configured server paths
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(serverPath, filePath);
                if (_systemFile.FileExists(fullPath))
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.ErrTheFileNameAlreadyExists,
                        Message = "File already exists"
                    };
                }
            }

            return new ResultInfo();
        }

        private string GetAssetFolder(int moduleId, string targetCountryCode, string marketId)
        {
            // Get readable names for folder structure
            var targetCountryName = GetTargetCountryName(targetCountryCode);
            var languageName = GetLanguageName(marketId);
            var moduleName = GetModuleName(moduleId);

            // Create folder structure: Asset/TargetCountry/Language/ModuleName
            return $"Asset/{targetCountryName}/{languageName}/{moduleName}";
        }

        private string GetTargetCountryName(string targetCountryCode)
        {
            var targetCountries = _landingPageService.GetTargetCountryList();
            var targetCountry = targetCountries.FirstOrDefault(tc => 
                string.Equals(tc.TargetCountryCode, targetCountryCode, StringComparison.OrdinalIgnoreCase));
            
            return targetCountry?.TargetCountryName ?? targetCountryCode;
        }

        private string GetLanguageName(string marketId)
        {
            var languages = _landingPageService.GetLanguageList();
            var language = languages.FirstOrDefault(l => 
                string.Equals(l.LanguageCode, marketId, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(l.ShortedLanguageCode, marketId, StringComparison.OrdinalIgnoreCase));
            
            return language?.LanguageName ?? marketId;
        }

        private string GetModuleName(int moduleId)
        {
            var modules = _landingPageService.GetModuleList();
            var module = modules.FirstOrDefault(m => m.ModuleId == moduleId);
            
            return module?.ModuleName ?? moduleId.ToString();
        }

        private static void BuildFilesXml(UploadAssetsParam param)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            for (var i = 0; i < param.FilePaths.Count; i++)
            {
                var row = $"<row MediaName=\"{param.MediaNames[i]}\" FileName=\"{param.FilePaths[i]}\" Dimension=\"{param.Dimensions[i]}\" />";
                result.Append(row);
            }

            result.Append("</root>");
            param.Files = result.ToString();
        }

        private void UploadFile(HttpPostedFileBase file, string fileName)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var filePath = Path.Combine(serverPath, fileName);
                
                // Ensure directory exists
                var directory = Path.GetDirectoryName(filePath);
                _systemFile.EnsureDirectoryExists(directory);
                
                // Save the file
                _systemFile.SaveFile(file, filePath);
            }
        }

        private void DeleteFile(string filePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.DeleteFile(serverPath, filePath);
            }
        }

        private ResultInfo InsertAssets(UploadAssetsParam param)
        {
            var command = new UploadAssetsCommand
            {
                Files = param.Files,
                ModuleId = param.ModuleId,
                TargetCountryCode = param.TargetCountryCode,
                MarketId = param.MarketId,
                FunctionName = param.FunctionName,
                UserId = param.UserId,
                Ip = param.Ip
            };

            return ExecuteReturnResultInfo(command);
        }

        private ResultInfo ValidationInputFileName(HttpPostedFileBase file, UploadAssetsParam param)
        {
            if (!string.IsNullOrEmpty(param.InputFileNames))
            {
                if (RegexHelper.IsMatch(param.InputFileNames, RestrictedMultiInputFileNamesPattern))
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = Lang.FileNameCanNotContain
                    };
                }

                var extension = _systemFile.GetExtension(param.InputFileNames).ToLower(System.Globalization.CultureInfo.InvariantCulture);
                if (!LandingPageConstants.FileExtensions.AllImageExtensions.Contains(extension))
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = LandingPageConstants.ErrorMessages.InvalidFileType
                    };
                }
            }
            else if (file != null)
            {
                param.InputFileNames = file.FileName;
            }
            else
            {
                // No file and no input file name provided - this is handled by validation
            }

            return param.InputFileNames != null && param.InputFileNames.Length > MaxLength ? new ResultInfo
            {
                Code = ErrCode.GeneralErrorCode,
                Message = "The maximum length of file name is 50."
            } : new ResultInfo();
        }

        private ResultInfo CheckFileInFolderForUpdate(string filePath, string oldFilePath = null)
        {
            // Check if file already exists in any of the configured server paths
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(serverPath, filePath);
                if (_systemFile.FileExists(fullPath))
                {
                    // If updating and the file path is the same as old file, it's allowed
                    if (!string.IsNullOrEmpty(oldFilePath) && 
                        string.Equals(filePath, oldFilePath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new ResultInfo();
                    }

                    return new ResultInfo
                    {
                        Code = ErrCode.ErrTheFileNameAlreadyExists,
                        Message = "File already exists"
                    };
                }
            }

            return new ResultInfo();
        }

        private ResultInfo ExecuteUpdateAsset(UploadAssetsParam param)
        {
            var command = new UpdateModuleMediaCommand
            {
                MediaId = param.MediaId,
                ModuleId = param.ModuleId,
                MediaName = string.Join("|", param.MediaNames),
                FileName = string.Join("|", param.FilePaths),
                Size = string.Join("|", param.Dimensions),
                UserId = param.UserId,
                Ip = param.Ip
            };

            return ExecuteReturnResultInfo(command);
        }

        private void MoveFile(string newFilePath, string oldFilePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.MoveFile(serverPath, newFilePath, oldFilePath);
            }
        }

        private ResultInfo DeleteAssets(DeleteMediasParam param)
        {
            // Get file names before deletion for cleanup
            var fileNameList = GetAssetFileNameList(param, param.MediaIds);
            
            var command = new DeleteMediasCommand
            {
                MediaIds = param.MediaIds,
                FunctionName = param.FunctionName,
                UserId = param.UserId,
                Ip = param.Ip
            };

            var result = ExecuteReturnResultInfo(command);

            if (result.IsSuccessful)
            {
                DeleteFiles(fileNameList);
            }
            
            return result;
        }

        private List<string> GetAssetFileNameList(AssetListParam param, string mediaIds)
        {
            var mediaIdList = mediaIds.Split(',');
            var fileNameList = new List<string>();

            foreach (var mediaId in mediaIdList)
            {
                if (int.TryParse(mediaId.Trim(), out var id))
                {
                    param.MediaId = id;
                    var assetInfo = GetAssetDetail(param);
                    if (assetInfo != null && !string.IsNullOrEmpty(assetInfo.FileName))
                    {
                        fileNameList.Add(assetInfo.FileName);
                    }
                }
            }

            return fileNameList;
        }

        private void DeleteFiles(List<string> fileList)
        {
            for (int i = 0; i < fileList.Count; i++)
            {
                DeleteFile(fileList[i]);
            }
        }

        #endregion
    }
}
