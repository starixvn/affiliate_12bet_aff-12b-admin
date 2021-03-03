using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class NewsLetterService : INewsLetterService
    {
        private const string ACCEPT_FILE_MIME = "text/html";
        private const string ACCEPT_FILE_EXTENSION = ".html";
        private const int MAX_FILE_SIZE = 3 * 1024 * 1024;
        private const int MAX_LENGTH = 50;

        private const string UPLOAD_FOLDER = "NewsLetter";
        

        private INewsLetterRepository Repo { get; }
        private ISystemFile SystemFile { get; }
        private IAppSetting AppSetting { get; }

        public NewsLetterService(INewsLetterRepository repo, ISystemFile systemFile, IAppSetting appSetting) 
        {
            Repo = repo;
            SystemFile = systemFile;
            AppSetting = appSetting;
        }


        public EntityResult<NewsLetter> GetNewsLetters(SelectNewsLetterParam param)
            => Repo.GetNewsLetters(param);

        public ResultInfo AddNewsLetter(HttpPostedFileBase file, InsertNewsLetterParam param)
        {
            var result = ValidateFile(file);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = ValidateFileName(param.FileName);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = CheckFileExistInFolder(param.FileName);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = UploadFile(file, param.FileName);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = InsertToDB(param);

            if (!result.IsSuccessful)
            {
                Delete(param.FileName);
            }

            return result;
        }

        public ResultInfo UpdateNewsLetter(HttpPostedFileBase file, UpdateNewsLetterParam param)
        {

            var result = ValidateFileName(param.FileName);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var oldNewsLetter = Repo.GetNewsLetterById(param.NewsLetterId);
            if (oldNewsLetter == null)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidId
                };
            }

            if (file != null)
            {
                result = ValidateFile(file);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                result = CheckFileExistInFolder(param.FileName);
                if (!result.IsSuccessful && oldNewsLetter.FileName != param.FileName)
                {
                    return result;
                }

                UploadFile(file, param.FileName);
                if(!string.Equals(oldNewsLetter.FileName, param.FileName, StringComparison.OrdinalIgnoreCase))
                {
                    Delete(oldNewsLetter.FileName);
                }

                return Repo.UpdateNewsLetter(param);
            }
            else
            {
                result = CheckFileExistInFolder(param.FileName);
                if (!result.IsSuccessful && oldNewsLetter.FileName != param.FileName)
                {
                    return result;
                }

                if (!string.Equals(oldNewsLetter.FileName, param.FileName, StringComparison.OrdinalIgnoreCase))
                {
                    MoveFile(param.FileName, oldNewsLetter.FileName);
                }

                return Repo.UpdateNewsLetter(param);
            }

        }

        public ResultInfo DeleteNewsLetters(DeleteNewsLetterParam param)
        {
            var fileNames = GetFileNamesFromIds(param.NewsLetterIds);
            var result = Repo.DeleteNewsLetter(param);

            if (result.IsSuccessful)
            {
                foreach (var fileName in fileNames)
                {
                    Delete(fileName);
                }
            }
            return result;
        }

        public NewsLetter GetNewsLetterById(int id)
            => Repo.GetNewsLetterById(id);

        private IEnumerable<string> GetFileNamesFromIds(string newsLetterIds)
        {
            return newsLetterIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Convert.ToInt32(i))
                .Select(i => GetNewsLetterById(i))
                .Select(item => item.FileName)
                .ToList();
        }

        private void MoveFile(string newFileName, string oldFileName)
        {
            foreach (var serverPath in AppSetting.BannerFolders)
            {
                var folder = Path.Combine(serverPath, UPLOAD_FOLDER);
                SystemFile.MoveFile(folder, newFileName, oldFileName);
            }
        }

        private ResultInfo InsertToDB(InsertNewsLetterParam param)
        {
            try
            {
                return Repo.InsertNewsLetter(param);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.ErrGeneral,
                };
            }
        }

        private void Delete(string fileName)
        {
            foreach (var serverPath in AppSetting.BannerFolders)
            {
                var folder = Path.Combine(serverPath, UPLOAD_FOLDER);
                SystemFile.DeleteFile(folder, fileName);
            }
        }

        private ResultInfo UploadFile(HttpPostedFileBase file, string fileName)
        {
            foreach (var serverPath in AppSetting.BannerFolders)
            {
                var filePath = Path.Combine(serverPath, UPLOAD_FOLDER, fileName);
                var result = SystemFile.UploadFile(file.InputStream, filePath);
                if (!result.IsSuccessful)
                {
                    return result;
                }
            }
            return ResultInfo.Success;
        }

        private ResultInfo CheckFileExistInFolder(string fileName)
        {
            foreach (var serverPath in AppSetting.BannerFolders)
            {
                var result = SystemFile.CreateFolder(serverPath, UPLOAD_FOLDER);
                if (!result.IsSuccessful)
                {
                    return result;
                }
                var folderPath = Path.Combine(serverPath, UPLOAD_FOLDER);
                result = SystemFile.CheckExistedFile(folderPath, fileName);
                if (!result.IsSuccessful)
                {
                    return result;
                }    
            }
            return ResultInfo.Success;
        }

        private ResultInfo ValidateFileName(string fileName)
        {
            var regex = new Regex("[\\\\/:*?\"<>#%&+]");
            if (regex.IsMatch(fileName))
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.FileNameCanNotContain
                };
            }

            if (Path.GetExtension(fileName) != ACCEPT_FILE_EXTENSION)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidFileType
                };
            }

            if (fileName.Length > MAX_LENGTH)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.MaxLenghtOfFileName
                };
            }

            return ResultInfo.Success;
        }

        private ResultInfo ValidateFile(HttpPostedFileBase file)
        {
            if (file.ContentType != ACCEPT_FILE_MIME)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidFileType
                };
            }

            if (file.ContentLength > MAX_FILE_SIZE)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidFileSize
                };
            }
            return ResultInfo.Success;
        }


    }
}
