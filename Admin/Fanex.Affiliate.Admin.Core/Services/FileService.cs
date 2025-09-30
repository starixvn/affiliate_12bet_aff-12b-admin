using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Utilities.MediaGallery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class FileService : IFileService
    {
        private const int MaxLength = 200;
        private const string RestrictedMultiInputFileNamesPattern = "[\\\\/:*?\"<>#%&+]";
        private const string DuplicateFileNameMessage = "{0} Name is unique, please enter other {0} name!";
        private const string ExistedFileNamesMessage = "File name(s) already exists: {0}. Please rename or remove the file(s). before uploading.";

        private readonly IAppSetting _appSetting;
        private readonly ISystemFile _systemFile;

        public FileService(
            IAppSetting appSetting,
            ISystemFile systemFile)
        {
            _appSetting = appSetting;
            _systemFile = systemFile;
        }

        public ResultInfo CheckFileInFolder(string filePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(serverPath, filePath);
                if (_systemFile.FileExists(fullPath))
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.ErrTheFileNameAlreadyExists
                    };
                }
            }

            return new ResultInfo();
        }

        public void CleanupUploadedFiles(List<string> filePaths)
        {
            foreach (var path in filePaths)
            {
                DeleteFile(path);
            }
        }

        public ResultInfo CheckMultipleFilesInFolder(string[] inputFileNames, string folder)
        {
            var result = new ResultInfo();
            var existedFileNames = new List<string>();

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
                    existedFileNames.Add(inputFileName);
                }
            }

            result.Message = result.IsSuccessful ? string.Empty
                : string.Format(ExistedFileNamesMessage, string.Join(", ", existedFileNames));

            return result;
        }

        public string[] PrepareInputFileNames(HttpPostedFileBase[] files, string inputFileNames)
        {
            return string.IsNullOrEmpty(inputFileNames)
                ? files.Select(f => f.FileName).ToArray()
                : inputFileNames.Split('|');
        }

        public void DeleteFile(string filePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.DeleteFile(serverPath, filePath);
            }
        }

        public void UploadFile(HttpPostedFileBase file, string fileName)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var filePath = Path.Combine(serverPath, fileName);

                _systemFile.SaveFile(file, filePath);
            }
        }

        public ResultInfo ValidateAllInputFileNames(string[] inputFileNames, HttpPostedFileBase[] files)
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

        public ResultInfo CheckDuplicateFileNames(string[] inputFileNames, string nameType = "File")
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

        public ResultInfo ValidateUploadRequest(HttpPostedFileBase[] files, string inputFileNames, string inputMediaNames = null)
        {
            var result = ValidateAllFiles(files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            if (RegexHelper.IsMatch(inputFileNames, RestrictedMultiInputFileNamesPattern) ||
                (!string.IsNullOrEmpty(inputMediaNames) && RegexHelper.IsMatch(inputMediaNames, RestrictedMultiInputFileNamesPattern)))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = Lang.FileNameCanNotContain
                };
            }

            return new ResultInfo();
        }

        public ResultInfo ValidateProvidedInputFileName(string inputFileName, int index, StringBuilder message)
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
                message.Append($"The maximum length of file name is 200. Please choose another name File Name {index + 1}.<br />");
            }

            return result;
        }

        public ResultInfo ValidateAllFiles(HttpPostedFileBase[] files)
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

        public ResultInfo ValidateFile(string fileName, int contentLength, Stream inputStream)
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
                var validationResult = ImageDimensionHelper.ValidateImageDimensionsDetailed(
                    inputStream,
                    LandingPageConstants.FileUpload.MaxImageWidth,
                    LandingPageConstants.FileUpload.MaxImageHeight);

                if (!validationResult.IsValid)
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = string.IsNullOrEmpty(validationResult.ErrorMessage)
                            ? validationResult.GetValidationMessage()
                            : LandingPageConstants.ErrorMessages.InvalidImageData
                    };
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
                message.Append($"The maximum length of file name is 200. Please choose another name for File Name {index + 1} or select another File.<br />");
                return new ResultInfo { Code = ErrCode.GeneralErrorCode };
            }

            return new ResultInfo();
        }
    }
}
