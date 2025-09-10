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
using System.Text.RegularExpressions;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class AssetUploaderService : BaseDynamicService, IAssetUploaderService
    {
        private const int MaxLength = 50;
        private const string RestrictedMultiInputFileNamesPattern = "[\\\\/:*?\"<>#%&+]";
        private const string DefaultDimensions = "0x0";
        private const string AssetNotFoundMessage = "Asset not found.";
        private const string FileAlreadyExistsMessage = "File already exists";
        private const string DatabaseInsertionFailsMessage = "Database insertion fails";
        private const string MaxLengthFileNameMessage = "The maximum length of file name is 50.";
        private const string MaxLengthMediaNameMessage = "The maximum length of media name is 50.";
        private const string MaxLengthFormNameMessage = "The maximum length of form name is 50. Please choose another name for Form Name {0} or select another Form.<br />";
        private const string DuplicateFileNameMessage = "{0} Name is unique, please enter other {0} name!";
        private const string ExistingAssetsMessage = "This file name(s) belong to existing asset(s). Please change the file name.";

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

        /// <summary>
        /// Uploads multiple asset files with validation and database storage
        /// </summary>
        /// <param name="files">Array of files to upload</param>
        /// <param name="param">Upload parameters containing file names, media names, and metadata</param>
        /// <returns>Result information indicating success or failure with details</returns>
        public ResultInfo UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            var result = ValidateUploadRequest(files, param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var inputFileNames = PrepareInputFileNames(files, param);
            var inputMediaNames = PrepareInputMediaNames(inputFileNames, param);

            result = ValidateInputNames(inputFileNames, files, inputMediaNames);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var folder = GetAssetFolder(param.ModuleId, param.TargetCountryCode, param.MarketId);
            result = CheckMultipleFilesInFolder(inputFileNames, folder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            ProcessAndUploadFiles(files, inputFileNames, inputMediaNames, folder, param);

            return SaveAssetsToDatabase(param);
        }

        /// <summary>
        /// Updates an existing asset with new file content or metadata
        /// </summary>
        /// <param name="file">New file to upload (optional for metadata-only updates)</param>
        /// <param name="param">Update parameters containing file names, media names, and asset ID</param>
        /// <returns>Result information indicating success or failure with details</returns>
        public ResultInfo UpdateAsset(HttpPostedFileBase file, UpdateAssetParam param)
        {
            var result = ValidateUpdateAssetInput(file, param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var oldAssetInfo = GetAssetDetail(new AssetDetailParam { MediaId = param.MediaId, UserId = param.UserId });
            if (oldAssetInfo == null)
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = AssetNotFoundMessage
                };
            }

            return ProcessAssetUpdate(file, param, oldAssetInfo);
        }

        private ResultInfo ProcessAssetUpdate(HttpPostedFileBase file, UpdateAssetParam param, LandingPageAsset oldAssetInfo)
        {
            var oldFilePath = oldAssetInfo.FileName;
            var folder = GetAssetFolder(param.ModuleId, param.TargetCountryCode, param.MarketId);
            var inputFileName = string.IsNullOrEmpty(param.InputFileNames) ? file?.FileName : param.InputFileNames;
            var inputMediaName = param.InputMediaNames;
            var path = $"{folder}/{inputFileName}";

            var result = CheckFileInFolderForUpdate(path, oldFilePath);
            if (!result.IsSuccessful)
            {
                return result;
            }

            param.FilePaths.Add(path);
            
            if (string.IsNullOrEmpty(inputMediaName))
            {
                inputMediaName = param.FunctionName.MatchWith(Function.RegistrationFormUploader) ?
                    CreateFileExtensionRemovalRegex().Replace(inputFileName, string.Empty) : inputFileName;
            }
            
            param.MediaNames.Add(inputMediaName);

            return file != null 
                ? ProcessFileUpdate(file, param, path, oldFilePath)
                : ProcessMetadataUpdate(param, path, oldFilePath, oldAssetInfo);
        }

        private ResultInfo ProcessFileUpdate(HttpPostedFileBase file, UpdateAssetParam param, string path, string oldFilePath)
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

        private ResultInfo ProcessMetadataUpdate(UpdateAssetParam param, string path, string oldFilePath, LandingPageAsset oldAssetInfo)
        {
            param.Dimensions.Add(oldAssetInfo.Dimension ?? DefaultDimensions);
            BuildFilesXml(param);
            var result = ExecuteUpdateAsset(param);

            if (result.IsSuccessful && !string.Equals(path, oldFilePath, StringComparison.CurrentCultureIgnoreCase))
            {
                MoveFile(path, oldFilePath);
                DeleteFile(oldFilePath);
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
                return DefaultDimensions;
            }
        }

        private static Regex CreateFileExtensionRemovalRegex()
        {
            var extensionPattern = CreateFileExtensionPattern();
            return new Regex(@"\.(" + extensionPattern + ")$", RegexOptions.IgnoreCase);
        }

        private static string CreateFileExtensionPattern()
        {
            return string.Join("|", LandingPageConstants.FileExtensions.AllImageExtensions
                .Select(ext => ext.StartsWith(".") ? ext.Substring(1) : ext));
        }

        /// <summary>
        /// Retrieves a paginated list of assets based on filter criteria
        /// </summary>
        /// <param name="param">Filter parameters for asset search</param>
        /// <returns>Paginated result containing matching assets</returns>
        public EntityResult<LandingPageAsset> GetAssetList(AssetUploaderParam param)
        {
            var criteria = param.MapTo<AssetUploaderParam, GetAssetUploaderCriteria>();

            return FetchEntityResult<LandingPageAsset>(criteria);
        }

        /// <summary>
        /// Retrieves detailed information for a specific asset
        /// </summary>
        /// <param name="param">Parameters containing asset ID and user ID</param>
        /// <returns>Asset details or null if not found</returns>
        public LandingPageAsset GetAssetDetail(AssetDetailParam param)
        {
            var criteria = param.MapTo<AssetDetailParam, GetAssetDetailCriteria>();

            return GetTData<LandingPageAsset>(criteria);
        }

        /// <summary>
        /// Toggles the active status of one or more assets
        /// </summary>
        /// <param name="param">Parameters containing media IDs and new status</param>
        /// <returns>Result information indicating success or failure</returns>
        public ResultInfo ToggleAssetStatus(UpdateMediasStatusParam param)
        {
            var command = param.MapTo<UpdateMediasStatusParam, UpdateMediasStatusCommand>();

            return ExecuteReturnResultInfo(command);
        }

        /// <summary>
        /// Deletes one or more assets from both database and file system
        /// </summary>
        /// <param name="param">Parameters containing media IDs to delete</param>
        /// <returns>Result information indicating success or failure</returns>
        public ResultInfo DeleteMedias(DeleteMediasParam param)
        {
            return DeleteAssets(param);
        }

        #region Private Helper Methods

        private ResultInfo ValidateUploadRequest(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            var result = ValidateAllFiles(files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            if (RegexHelper.IsMatch(param.InputFileNames, RestrictedMultiInputFileNamesPattern) || 
                RegexHelper.IsMatch(param.InputMediaNames, RestrictedMultiInputFileNamesPattern))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = Lang.FileNameCanNotContain
                };
            }

            return new ResultInfo();
        }

        private static string[] PrepareInputFileNames(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            return string.IsNullOrEmpty(param.InputFileNames) 
                ? files.Select(f => f.FileName).ToArray() 
                : param.InputFileNames.Split('|');
        }

        private static string[] PrepareInputMediaNames(string[] inputFileNames, UploadAssetsParam param)
        {
            return string.IsNullOrEmpty(param.InputMediaNames) 
                ? inputFileNames 
                : param.InputMediaNames.Split('|');
        }

        private ResultInfo ValidateInputNames(string[] inputFileNames, HttpPostedFileBase[] files, string[] inputMediaNames)
        {
            var result = ValidateAllInputFileNames(inputFileNames, files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            return ValidateAllInputMediaNames(inputMediaNames);
        }

        private void ProcessAndUploadFiles(HttpPostedFileBase[] files, string[] inputFileNames, string[] inputMediaNames, string folder, UploadAssetsParam param)
        {
            var extensionRemovalRegex = CreateFileExtensionRemovalRegex();

            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var fileName = inputFileNames[i];
                var path = $"{folder}/{fileName}";

                var dimensions = ExtractImageDimensions(file.InputStream);

                string mediaName = inputMediaNames[i];
                if (string.IsNullOrEmpty(mediaName))
                {
                    mediaName = extensionRemovalRegex.Replace(fileName, string.Empty);
                }

                param.MediaNames.Add(mediaName);
                param.FilePaths.Add(path);
                param.Dimensions.Add(dimensions);

                UploadFile(file, path);
            }
        }

        private ResultInfo SaveAssetsToDatabase(UploadAssetsParam param)
        {
            BuildFilesXml(param);

            try
            {
                var result = InsertAssets(param);
                if (!result.IsSuccessful)
                {
                    CleanupUploadedFiles(param.FilePaths);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                CleanupUploadedFiles(param.FilePaths);
                return ResultInfo.GenerateErr(DatabaseInsertionFailsMessage);
            }
        }

        private void CleanupUploadedFiles(List<string> filePaths)
        {
            foreach (var path in filePaths)
            {
                DeleteFile(path);
            }
        }

        private ResultInfo ValidateAllFiles(HttpPostedFileBase[] files)
        {
            var result = new ResultInfo();
            var message = new StringBuilder();
            
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var validationResult = ValidateFile(fileName, file.ContentLength, file.InputStream);

                if (!validationResult.IsSuccessful)
                {
                    result.Code = validationResult.Code;
                    message.Append($"File {fileName} is incorrect.<br />" + validationResult.Message);
                }
            }

            result.Message = message.ToString();
            return result;
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

        private ResultInfo CheckDuplicateFileNames(string[] inputFileNames, string nameType = "File")
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
                            Message = string.Format(DuplicateFileNameMessage, nameType)
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
                var validationResult = ValidateSingleInputFileName(inputFileNames, files, i, message);
                if (!validationResult.IsSuccessful)
                {
                    result.Code = validationResult.Code;
                }
            }

            result.Message = message.ToString();
            return result;
        }

        private ResultInfo ValidateSingleInputFileName(string[] inputFileNames, HttpPostedFileBase[] files, int index, StringBuilder message)
        {
            var inputFileName = inputFileNames[index];

            if (string.IsNullOrEmpty(inputFileName))
            {
                return ValidateEmptyInputFileName(inputFileNames, files, index, message);
            }

            return ValidateProvidedInputFileName(inputFileName, index, message);
        }

        private ResultInfo ValidateEmptyInputFileName(string[] inputFileNames, HttpPostedFileBase[] files, int index, StringBuilder message)
        {
            var file = files[index];
            inputFileNames[index] = file.FileName;

            if (file.FileName.Length > MaxLength)
            {
                message.Append($"The maximum length of file name is 50. Please choose another name for File Name {index + 1} or select another File.<br />");
                return new ResultInfo { Code = ErrCode.GeneralErrorCode };
            }

            return new ResultInfo();
        }

        private ResultInfo ValidateProvidedInputFileName(string inputFileName, int index, StringBuilder message)
        {
            var result = new ResultInfo();

            var extension = _systemFile.GetExtension(inputFileName).ToLower(System.Globalization.CultureInfo.InvariantCulture);
            if (!LandingPageConstants.FileExtensions.AllImageExtensions.Contains(extension))
            {
                result.Code = ErrCode.GeneralErrorCode;
                message.Append($"File Name {index + 1} is incorrect.<br />" + LandingPageConstants.ErrorMessages.InvalidFileType);
            }

            if (inputFileName.Length > MaxLength)
            {
                result.Code = ErrCode.GeneralErrorCode;
                message.Append($"The maximum length of file name is 50. Please choose another name File Name {index + 1}.<br />");
            }

            return result;
        }

        private ResultInfo ValidateAllInputMediaNames(string[] inputMediaNames)
        {
            var result = CheckDuplicateFileNames(inputMediaNames, "Form");
            if (!result.IsSuccessful)
            {
                return result;
            }

            var message = new StringBuilder();
            for (int i = 0; i < inputMediaNames.Length; i++)
            {
                var inputMediaName = inputMediaNames[i];

                if (!string.IsNullOrEmpty(inputMediaName) && inputMediaName.Length > MaxLength)
                {
                    result.Code = ErrCode.GeneralErrorCode;
                    message.Append(string.Format(MaxLengthFormNameMessage, i + 1));
                }       
            }

            result.Message = message.ToString();
            return result;
        }

        private ResultInfo CheckMultipleFilesInFolder(string[] inputFileNames, string folder)
        {
            var result = new ResultInfo();
            var message = new StringBuilder();

            foreach (var inputFileName in inputFileNames)
            {
                var filePath = $"{folder}/{inputFileName}";
                var checkResult = CheckFileInFolder(filePath);

                if (!checkResult.IsSuccessful)
                {
                    if (checkResult.Code == ErrCode.ErrCanNotAccessFolder)
                    {
                        return checkResult;
                    }

                    result.Code = ErrCode.GeneralErrorCode;
                    message.Append($"{inputFileName}, ");
                }
            }

            result.Message = result.IsSuccessful ? string.Empty
                : message.Append(ExistingAssetsMessage).ToString();

            return result;
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
                        Message = FileAlreadyExistsMessage
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
            return  HttpUtility.HtmlEncode($"Asset/{targetCountryName}/{languageName}/{moduleName}");
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
            var moduleName = module?.ModuleName ?? moduleId.ToString();

            if (!string.IsNullOrEmpty(moduleName))
            {
                moduleName = moduleName.Replace(" ", string.Empty).Replace("&", "_");
            }

            return moduleName;
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
                var directory = Path.GetDirectoryName(filePath);
                _systemFile.EnsureDirectoryExists(directory);

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
            var command = param.MapTo<UploadAssetsParam, UploadAssetsCommand>();

            return ExecuteReturnResultInfo(command);
        }

        private ResultInfo ValidateUpdateAssetInput(HttpPostedFileBase file, UploadAssetsParam param)
        {
            var result = ValidateInputFileNameForUpdate(file, param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            return ValidateInputMediaNameForUpdate(param);
        }

        private ResultInfo ValidateInputFileNameForUpdate(HttpPostedFileBase file, UploadAssetsParam param)
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

                if (param.InputFileNames.Length > MaxLength)
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = MaxLengthFileNameMessage
                    };
                }
            }
            else {
                if (file != null)
                {
                    param.InputFileNames = file.FileName;
                }
            }

            return new ResultInfo();
        }

        private ResultInfo ValidateInputMediaNameForUpdate(UploadAssetsParam param)
        {
            if (!string.IsNullOrEmpty(param.InputMediaNames))
            {
                if (RegexHelper.IsMatch(param.InputMediaNames, RestrictedMultiInputFileNamesPattern))
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = Lang.FileNameCanNotContain
                    };
                }

                if (param.InputMediaNames.Length > MaxLength)
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = MaxLengthMediaNameMessage
                    };
                }
            }

            return new ResultInfo();
        }

        private ResultInfo CheckFileInFolderForUpdate(string filePath, string oldFilePath = null)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(serverPath, filePath);
                if (_systemFile.FileExists(fullPath))
                {
                    if (!string.IsNullOrEmpty(oldFilePath) && 
                        string.Equals(filePath, oldFilePath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new ResultInfo();
                    }

                    return new ResultInfo
                    {
                        Code = ErrCode.ErrTheFileNameAlreadyExists,
                        Message = FileAlreadyExistsMessage
                    };
                }
            }

            return new ResultInfo();
        }

        private ResultInfo ExecuteUpdateAsset(UpdateAssetParam param)
        {
            var command = param.MapTo<UpdateAssetParam, UpdateAssetCommand>();

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
            var fileNameList = GetAssetFileNameList(param.UserId, param.MediaIds);
            
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

        private List<string> GetAssetFileNameList(int userId, string mediaIds)
        {
            var mediaIdList = mediaIds.Split(',');
            var fileNameList = new List<string>();
            var param = new AssetDetailParam { UserId = userId };

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
            foreach (var filePath in fileList)
            {
                DeleteFile(filePath);
            }
        }

        #endregion
    }
}
