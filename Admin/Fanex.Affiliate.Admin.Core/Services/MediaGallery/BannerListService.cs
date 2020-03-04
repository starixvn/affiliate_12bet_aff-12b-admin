using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Utilities.MediaGallery;
using Fanex.Affiliate.Admin.Core.Configurations;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Drawing;
using System.Web;
using System.IO;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class BannerListService : IBannerListService
    {
        private const int MaxLength = 50;
        private readonly IBannerListRepository _bannerListRepository;
        private readonly IAppSetting _appSetting;
        private readonly ISystemFile _systemFile;
        private readonly IBannerValidationService _bannerValidationService;
        public BannerListService(
            IBannerListRepository bannerListRepository,
            IAppSetting appSetting,
            ISystemFile systemFile,
            IBannerValidationService bannerValidationService)
        {
            _bannerListRepository = bannerListRepository;
            _appSetting = appSetting;
            _systemFile = systemFile;
            _bannerValidationService = bannerValidationService;
        }

        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            param.Category = GetCategory(param.Category, param.SubCategory);

            return _bannerListRepository.GetBannerList(param);
        }

        public BannerInfo GetBannerDetail(int selectedBannerId)
        {
            return _bannerListRepository.GetBannerDetail(selectedBannerId);
        }

        private ResultInfo ValidateAllFiles(HttpPostedFileBase[] files)
        {
            var resultValidateAllFiles = new ResultInfo();
            var message = new StringBuilder();
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var result = _bannerValidationService.ValidateFile(fileName, file.ContentLength);

                if (!result.IsSuccessful)
                {
                    resultValidateAllFiles.Code = result.Code;
                    result.Message = $"File {fileName} is incorrect.<br />" + result.Message;
                    message.Append(result.Message);
                }
            }

            resultValidateAllFiles.Message = message.ToString();
            return resultValidateAllFiles;
        }

        private ResultInfo ValidateAllInputFileNames(string[] inputFileNames, HttpPostedFileBase[] files)
        {
            var resultValidateAllInputFileNames = new ResultInfo();
            var message = new StringBuilder();
            for (int i = 0; i < inputFileNames.Length; i ++)
            {
                var inputFileName = inputFileNames[i];

                if (string.IsNullOrEmpty(inputFileNames[i]))
                {
                    var file = files[i];

                    inputFileNames[i] = file.FileName;

                    if (file.FileName.Length > MaxLength)
                    {
                        resultValidateAllInputFileNames.Code = Code.GeneralErrorCode;
                        message.Append($"The maximum length of file name is 50. Please choose another name for File Name {i + 1} or select another File.<br />");
                    }                    
                } else
                {
                    if (!BannerHelper.IsValidExtension(_systemFile.GetExtension(inputFileName)))
                    {
                        resultValidateAllInputFileNames.Code = Code.GeneralErrorCode;
                        message.Append($"File Name {i + 1} is incorrect.<br />" + Lang.InvalidBannerExtensionFile);
                    }

                    if (inputFileName.Length > MaxLength)
                    {
                        resultValidateAllInputFileNames.Code = Code.GeneralErrorCode;
                        message.Append($"The maximum length of file name is 50. Please choose another name File Name {i + 1}.<br />");
                    }
                }
            }

            resultValidateAllInputFileNames.Message = message.ToString();
            return resultValidateAllInputFileNames;
        }

        public ResultInfo CheckMultipleFilesInFolder(string[] inputFileNames)
        {
            var resultCheckMultipleFilesInFolder = new ResultInfo();
            var message = new StringBuilder();

            for (int i = 0; i < inputFileNames.Length; i++)
            {
                var inputFileName = inputFileNames[i];
                var filePath = $"CategoryMedia/{inputFileName}";
                var result = _bannerValidationService.CheckFileInFolder("CategoryMedia/", filePath);

                if(!result.IsSuccessful)
                {
                    if (result.Code == Code.ErrCanNotAccessFolder)
                    {
                        return result;
                    }

                    resultCheckMultipleFilesInFolder.Code = Code.GeneralErrorCode;
                    message.Append($"{inputFileName}, ");
                }
            }

            resultCheckMultipleFilesInFolder.Message = resultCheckMultipleFilesInFolder.IsSuccessful ? string.Empty
                : message.Append("This file name(s) belong to existing banner(s). Please change the file name.").ToString();

            return resultCheckMultipleFilesInFolder;
        }

        public static void BuildFilesXml(InsertUpdateMultipleBannersParam param, string[] inputFileNames)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            for (var i = 0; i < inputFileNames.Length; i++)
            {
                var row = $"<row FileName=\"{param.FilePaths[i]}\" Size=\"{param.Sizes[i]}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            param.Files = result.ToString();
        }

        public ResultInfo InsertBanner(HttpPostedFileBase[] files, InsertUpdateMultipleBannersParam param)
        {
            var result = ValidateAllFiles(files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var inputFileNames = param.InputFileNames.Split(';');
            result = ValidateAllInputFileNames(inputFileNames, files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = CheckMultipleFilesInFolder(inputFileNames);
            if (!result.IsSuccessful)
            {
                return result;
            }

            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                    
                using (var bitmap = new Bitmap(file.InputStream))
                {
                    var inputFileName = inputFileNames[i];
                    var path = $"CategoryMedia/{inputFileName}";
                    param.FilePaths.Add(path);
                    param.Sizes.Add(BannerHelper.GetSize(bitmap.Width, bitmap.Height));
                    UploadFile(bitmap, path);
                }
            }

            var resultInsert = _bannerListRepository.InsertBanner(param);

            if (!resultInsert.IsSuccessful)
            {
                foreach(var path in param.FilePaths)
                {
                    DeleteFile(path);
                }
            }

            return result;
        }

        public ResultInfo ValidationInputFileName(HttpPostedFileBase file, InsertUpdateMultipleBannersParam param)
        {
            if (file == null && string.IsNullOrEmpty(param.InputFileNames))
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "You must enter a file name for banner or choose a file."
                };
            }

            if (!string.IsNullOrEmpty(param.InputFileNames))
            {
                if (!BannerHelper.IsValidExtension(_systemFile.GetExtension(param.InputFileNames)))
                {
                    return BannerHelper.CreateErrorResult();
                }

                if (param.InputFileNames.Length > MaxLength)
                {
                    return new ResultInfo
                    {
                        Code = Code.GeneralErrorCode,
                        Message = "The maximum length of file name is 50. Please choose another file name or select another file."
                    };
                }
            }
            else
            {
                param.InputFileNames = file?.FileName;

                if (param.InputFileNames.Length > MaxLength)
                {
                    return new ResultInfo
                    {
                        Code = Code.GeneralErrorCode,
                        Message = "The maximum length of file name is 50. Please choose another file name or select another file."
                    };
                }
            }

            return new ResultInfo();
        }

        public ResultInfo UpdateBanner(HttpPostedFileBase file, InsertUpdateMultipleBannersParam param)
        {
            var result = ValidationInputFileName(file, param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var oldBannerInfo = _bannerListRepository.GetBannerDetail(param.BannerId);
            var oldFilePath = oldBannerInfo.FileName;

            result = CheckFileInFolder(param, oldFilePath);

            if (!result.IsSuccessful)
            {
                return result;
            }

            if (file != null)
            {
                result = _bannerValidationService.ValidateFile(file.FileName, file.ContentLength);

                if (result.IsSuccessful)
                {
                    using (var bitmap = new Bitmap(file.InputStream))
                    {
                        param.Sizes.Add(BannerHelper.GetSize(bitmap.Width, bitmap.Height));
                        BuildFilesXml(param, param.InputFileNames.Split(';'));
                        result = _bannerListRepository.UpdateBanner(param);

                        if (result.IsSuccessful)
                        {
                            DeleteFile(oldFilePath);
                            UploadFile(bitmap, param.FilePaths[0]);
                        }
                    }
                }
            }
            else
            {
                param.Sizes.Add(oldBannerInfo.Size);
                BuildFilesXml(param, param.InputFileNames.Split(';'));
                result = _bannerListRepository.UpdateBanner(param);

                if (result.IsSuccessful && string.Compare(param.FilePaths[0], oldFilePath, StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    MoveFile(param.FilePaths[0], oldFilePath);
                }
            }

            return new ResultInfo();
        }

        public ResultInfo DeleteBanners(DeleteBannerParam param)
        {
            var fileNameList = GetFileNameList(param.BannerIdList);
            var result = _bannerListRepository.DeleteBanners(param);

            if (result.IsSuccessful)
            {
                DeleteFiles(fileNameList);
            }
            return result;
        }

        public IEnumerable<MediaGalleryFilterItem> GetSizeList(SizeParam param)
        {
            param.Category = GetCategory(param.Category, param.SubCategory);

            return _bannerListRepository.GetSizeList(param);
        }

        private void UploadFile(Bitmap bitmap, string fileName)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var filePath = Path.Combine(serverPath, fileName);
                _systemFile.UploadFile(bitmap, filePath);
            }
        }

        private void DeleteFile(string filePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.DeleteFile(serverPath, filePath);
            }
        }

        private void DeleteFiles(List<string> fileList)
        {
            for (int i = 0; i < fileList.Count; i++)
            {
                DeleteFile(fileList[i]);
            }
        }

        private void MoveFile(string newFilePath, string oldFilePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.MoveFile(serverPath, newFilePath, oldFilePath);
            }
        }

        private List<string> GetFileNameList(string bannerIds)
        {
            var bannerIdList = bannerIds.Split(',');
            var fileNameList = new List<string>();

            foreach (var bannerId in bannerIdList)
            {
                var bannerInfo = _bannerListRepository.GetBannerDetail(Convert.ToInt32(bannerId));
                fileNameList.Add(bannerInfo.FileName);
            }

            return fileNameList;
        }

        private ResultInfo CheckFileInFolder(InsertUpdateMultipleBannersParam param, string oldFilePath)
        {
            var path = $"CategoryMedia/{param.InputFileNames}";
            var result = _bannerValidationService.CheckFileInFolder("CategoryMedia/", path);
            param.FilePaths.Add(path);

            if (result.Code == Code.ErrTheFileNameAlreadyExists && string.Compare(path, oldFilePath, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return new ResultInfo();
            }

            return result;
        }

        private static string GetCategory(string category, string subCategory)
        {
            if(string.IsNullOrEmpty(subCategory))
            {
                return category;
            }

            return subCategory;
        }
    }
}
