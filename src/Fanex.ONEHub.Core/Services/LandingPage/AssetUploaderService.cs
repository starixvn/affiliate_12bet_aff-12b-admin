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
using Fanex.Affiliate.Admin.Core.Utilities.MediaGallery;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class AssetUploaderService : BaseDynamicService, IAssetUploaderService
    {
        private const int MaxLength = 200;
        private const string RestrictedMultiInputFileNamesPattern = "[\\\\/:*?\"<>#%&+]";
        private const string DefaultDimensions = "0x0";
        private const string AssetNotFoundMessage = "Asset not found.";
        private const string FileAlreadyExistsMessage = "A file with this name {0} already exists on the server. Please use a different name.";
        private const string DatabaseInsertionFailsMessage = "Database insertion fails";
        private const string MaxLengthFileNameMessage = "The maximum length of file name is 200.";
        private const string MaxLengthMediaNameMessage = "The maximum length of media name is 200.";
        private const string MaxLengthFormNameMessage = "The maximum length of form name is 200. Please choose another name for Form Name {0} or select another Form.<br />";
        private const string InvalidLanguageForTargetCountryMessage = "The selected language is not valid for the chosen country. Please select a supported language.";

        private readonly IAppSetting _appSetting;
        private readonly ISystemFile _systemFile;
        private readonly IFileService _fileService;
        private readonly ILandingPageService _landingPageService;

        public AssetUploaderService(
            IDynamicRepository dynamicRepository,
            IAppSetting appSetting,
            ISystemFile systemFile,
            IFileService fileService,
            ILandingPageService landingPageService) : base(dynamicRepository)
        {
            _appSetting = appSetting;
            _systemFile = systemFile;
            _fileService = fileService;
            _landingPageService = landingPageService;
        }

        public ResultInfo UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            var result = _fileService.ValidateUploadRequest(files, param.InputFileNames, param.InputMediaNames);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var inputFileNames = _fileService.PrepareInputFileNames(files, param.InputFileNames);
            var inputMediaNames = PrepareInputMediaNames(inputFileNames, param.InputMediaNames);

            result = ValidateInputNames(inputFileNames, files, inputMediaNames);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = ValidateLanguageByTargetCountry(param.MarketId, param.TargetCountryCode);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var folder = GetAssetFolder(param.ModuleId, param.TargetCountryCode, param.MarketId);
            result = _fileService.CheckMultipleFilesInFolder(inputFileNames, folder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            ProcessAndUploadFiles(files, inputFileNames, inputMediaNames, folder, param);

            return SaveAssetsToDatabase(param);
        }

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

            var validationResult = ValidateLanguageByTargetCountry(param.MarketId, param.TargetCountryCode);
            if (!validationResult.IsSuccessful)
            {
                return validationResult;
            }

            return ProcessAssetUpdate(file, param, oldAssetInfo);
        }

        private ResultInfo ProcessAssetUpdate(HttpPostedFileBase file, UpdateAssetParam param, AssetDetail oldAssetInfo)
        {
            var oldFilePath = oldAssetInfo.FileName;
            var folder = GetAssetFolder(param.ModuleId, param.TargetCountryCode, param.MarketId);
            var inputFileName = string.IsNullOrEmpty(param.InputFileNames) ? file?.FileName : param.InputFileNames;
            var inputMediaName = param.InputMediaNames;
            var path = $"{folder}/{inputFileName}";

            var result = CheckFileInFolderForUpdate(path, oldFilePath, inputFileName);
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
            var result = _fileService.ValidateFile(file.FileName, file.ContentLength, file.InputStream);
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
                _fileService.DeleteFile(oldFilePath);
                _fileService.UploadFile(file, path);
            }

            return result;
        }

        private ResultInfo ProcessMetadataUpdate(UpdateAssetParam param, string path, string oldFilePath, AssetDetail oldAssetInfo)
        {
            param.Dimensions.Add(oldAssetInfo.Dimension ?? DefaultDimensions);
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
            return ImageDimensionHelper.ExtractImageDimensions(inputStream);
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

        public EntityResult<AssetDetail> GetAssetList(AssetUploaderParam param)
        {
            var criteria = param.MapTo<AssetUploaderParam, GetAssetUploaderCriteria>();

            return FetchEntityResult<AssetDetail>(criteria);
        }

        public AssetDetail GetAssetDetail(AssetDetailParam param)
        {
            var criteria = param.MapTo<AssetDetailParam, GetAssetDetailCriteria>();

            return GetTData<AssetDetail>(criteria);
        }

        public ResultInfo ToggleAssetStatus(UpdateAssetsStatusParam param)
        {
            var command = param.MapTo<UpdateAssetsStatusParam, UpdateAssetsStatusCommand>();

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteAssets(DeleteAssetsParam param)
        {
            var fileNameList = GetAssetFileNameList(param.UserId, param.MediaIds);

            var command = param.MapTo<DeleteAssetsParam, DeleteAssetsCommand>();

            var result = ExecuteReturnResultInfo(command);

            if (result.IsSuccessful)
            {
                DeleteFiles(fileNameList);
            }

            return result;
        }

        #region Private Helper Methods

        private ResultInfo ValidateLanguageByTargetCountry(string marketId, string targetCountryCode)
        {
            if (string.IsNullOrEmpty(marketId) || string.IsNullOrEmpty(targetCountryCode))
            {
                return ResultInfo.GenerateErr(InvalidLanguageForTargetCountryMessage);
            }

            var languages = _landingPageService.GetAllLanguagesInSystem();
            var isValidLanguage = languages.Any(l => 
                string.Equals(l.LanguageCode, marketId, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(l.TargetCountryCode, targetCountryCode, StringComparison.OrdinalIgnoreCase));

            if (!isValidLanguage)
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = InvalidLanguageForTargetCountryMessage
                };
            }

            return new ResultInfo();
        }

        private static string[] PrepareInputMediaNames(string[] inputFileNames, string inputMediaNames)
        {
            return string.IsNullOrEmpty(inputMediaNames) 
                ? inputFileNames 
                : inputMediaNames.Split('|');
        }

        private ResultInfo ValidateInputNames(string[] inputFileNames, HttpPostedFileBase[] files, string[] inputMediaNames)
        {
            var result = _fileService.ValidateAllInputFileNames(inputFileNames, files);
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

                _fileService.UploadFile(file, path);
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
                    _fileService.CleanupUploadedFiles(param.FilePaths);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                _fileService.CleanupUploadedFiles(param.FilePaths);
                return ResultInfo.GenerateErr(DatabaseInsertionFailsMessage);
            }
        }

        private ResultInfo ValidateAllInputMediaNames(string[] inputMediaNames)
        {
            var result = _fileService.CheckDuplicateFileNames(inputMediaNames, "Form");
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

        private ResultInfo CheckFileInFolderForUpdate(string filePath, string oldFilePath = null, string fileName = null)
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
                        Message = string.Format(FileAlreadyExistsMessage, fileName)
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
                _fileService.DeleteFile(filePath);
            }
        }

        #endregion
    }
}
