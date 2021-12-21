using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class HtmlFileService : IHtmlFileService
    {
        private const string ACCEPT_FILE_MIME = "text/html";
        private const string ACCEPT_FILE_EXTENSION = ".html";
        private const int MAX_FILE_SIZE = 3 * 1024 * 1024;
        private const int MAX_LENGTH = 50;

        private readonly ISystemFile _systemFile;
        private readonly IAppSetting _appSetting;

        public HtmlFileService(ISystemFile systemFile, IAppSetting appSetting)
        {
            _systemFile = systemFile;
            _appSetting = appSetting;
        }

        public ResultInfo HandleUploadFile(HttpPostedFileBase file, string fileName, string folder)
        {
            var result = ValidateFile(file);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = ValidateFileName(fileName);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = CheckFileExistInFolder(fileName, folder);

            if (!result.IsSuccessful)
            {
                return result;
            }

            return UploadFile(file, fileName, folder);
        }

        public void DeleteFiles(IEnumerable<string> fileNames, string folder)
        {
            foreach (var fileName in fileNames)
            {
                DeleteFile(fileName, folder);
            }
        }

        public void DeleteFile(string fileName, string folder)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var filePath = Path.Combine(serverPath, folder);
                _systemFile.DeleteFile(filePath, fileName);
            }
        }

        public ResultInfo ValidateFileName(string fileName)
        {
            var regex = new Regex("[\\\\/:*?$\"<>]");
            if (regex.IsMatch(fileName))
            {
                return ResultInfo.GenerateErr(Lang.FileNameMustNotContain);
            }

            if (Path.GetExtension(fileName) != ACCEPT_FILE_EXTENSION)
            {
                return ResultInfo.GenerateErr(Lang.InvalidFileType);
            }

            if (fileName.Length > MAX_LENGTH)
            {
                return ResultInfo.GenerateErr(Lang.MaxLenghtOfFileName);
            }

            return ResultInfo.Success;
        }

        public ResultInfo HandleUpdateFile(HttpPostedFileBase file, string oldFileName, string newFileName, string folder)
        {
            return file == null ? HandleRenameOldFile(oldFileName, newFileName, folder) :
                HandleReplaceFileByNewFile(file, oldFileName, newFileName, folder);
        }

        private ResultInfo HandleReplaceFileByNewFile(HttpPostedFileBase file, string oldFileName, string newFileName, string folder)
        {
            var result = ValidateFile(file);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = CheckFileExistInFolder(newFileName, folder);
            if (!result.IsSuccessful && oldFileName != newFileName)
            {
                return result;
            }

            result = UploadFile(file, newFileName, folder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            if (!string.Equals(oldFileName, newFileName, StringComparison.OrdinalIgnoreCase))
            {
                DeleteFile(oldFileName, folder);
            }

            return ResultInfo.Success;
        }

        private ResultInfo HandleRenameOldFile(string oldFileName, string newFileName, string folder)
        {
            var result = CheckFileExistInFolder(newFileName, folder);
            if (!result.IsSuccessful && oldFileName != newFileName)
            {
                return result;
            }

            if (!string.Equals(oldFileName, newFileName, StringComparison.OrdinalIgnoreCase))
            {
                MoveFile(newFileName, oldFileName, folder);
            }

            return ResultInfo.Success;
        }

        private ResultInfo UploadFile(HttpPostedFileBase file, string fileName, string folder)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var filePath = Path.Combine(serverPath, folder, fileName);
                var result = _systemFile.UploadFile(file.InputStream, filePath);
                if (!result.IsSuccessful)
                {
                    return result;
                }
            }
            return ResultInfo.Success;
        }

        private void MoveFile(string newFileName, string oldFileName, string folder)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var folderPath = Path.Combine(serverPath, folder);
                _systemFile.MoveFile(folderPath, newFileName, oldFileName);
            }
        }

        private ResultInfo CheckFileExistInFolder(string fileName, string folder)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var result = _systemFile.CreateFolder(serverPath, folder);
                if (!result.IsSuccessful)
                {
                    return result;
                }
                var folderPath = Path.Combine(serverPath, folder);
                result = _systemFile.CheckExistedFile(folderPath, fileName);
                if (!result.IsSuccessful)
                {
                    result.Message = "The html file name exist already, please choose another name.";
                    return result;
                }
            }
            return ResultInfo.Success;
        }

        private static ResultInfo ValidateFile(HttpPostedFileBase file)
        {
            if (file.ContentType != ACCEPT_FILE_MIME)
            {
                return ResultInfo.GenerateErr(Lang.InvalidFileType);
            }

            if (file.ContentLength > MAX_FILE_SIZE)
            {
                return ResultInfo.GenerateErr(Lang.InvalidFileSize);
            }
            return ResultInfo.Success;
        }
    }
}
