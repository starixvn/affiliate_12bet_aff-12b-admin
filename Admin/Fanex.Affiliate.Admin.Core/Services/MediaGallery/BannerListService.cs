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
                    resultValidateAllInputFileNames.Code = file.FileName.Length > 50 ? Code.GeneralErrorCode : resultValidateAllInputFileNames.Code;
                    message.Append($"The maximum length of file name is 50. Please choose another name for File Name {i + 1} or select another File.<br />");
                } else
                {
                    if (!BannerHelper.IsValidExtension(_systemFile.GetExtension(inputFileName)))
                    {
                        resultValidateAllInputFileNames.Code = Code.GeneralErrorCode;
                        message.Append($"File Name {i + 1} is incorrect.<br />" + Lang.InvalidBannerExtensionFile);
                    }

                    if (inputFileName.Length > 50)
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
                    message.Append($"{inputFileName}, ");

                    if (result.Code != Code.GeneralErrorCode)
                    {
                        return result;
                    }
                }
            }

            resultCheckMultipleFilesInFolder.Message = message.Append("This file name(s) belong to existing banner(s). Please change the file name.").ToString();
            return resultCheckMultipleFilesInFolder;
        }

        public ResultInfo InsertBanner(HttpPostedFileBase[] files, InsertMultipleBannersParam param)
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

            var message = new StringBuilder();

            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                    
                using (var bitmap = new Bitmap(file.InputStream))
                {
                    var inputFileName = inputFileNames[i];
                    param.FilePath = string.IsNullOrEmpty(inputFileName) ? $"CategoryMedia/{file.FileName}" : $"CategoryMedia/{inputFileName}";
                    param.Size = BannerHelper.GetSize(bitmap.Width, bitmap.Height);
                    var resultInsert = _bannerListRepository.InsertBanner(param);

                    if (resultInsert.IsSuccessful)
                    {
                        message.Append($"File Name {i + 1} insert succesfully!");
                        UploadFile(bitmap, param.FilePath);
                    } else
                    {
                        result.Code = resultInsert.Code;
                        message.Append($"File Name {i + 1} insert failed!");
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdateBanner(HttpPostedFileBase file, InsertUpdateBannerParam param)
        {
            if (!BannerHelper.IsValidExtension(_systemFile.GetExtension(param.InputFileName)))
            {
                return BannerHelper.CreateErrorResult();
            }

            var oldBannerInfo = _bannerListRepository.GetBannerDetail(param.BannerId);
            var oldFilePath = oldBannerInfo.FileName;
            param.Size = oldBannerInfo.Size;

            var result = CheckFileInFolder(param, oldFilePath);

            if(!result.IsSuccessful)
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
                        param.Size = BannerHelper.GetSize(bitmap.Width, bitmap.Height);
                        result = _bannerListRepository.UpdateBanner(param);

                        if (result.IsSuccessful)
                        {
                            DeleteFile(oldFilePath);
                            UploadFile(bitmap, param.FilePath);
                        }
                    }
                }
            }
            else
            {
                result = _bannerListRepository.UpdateBanner(param);

                if (result.IsSuccessful && string.Compare(param.FilePath, oldFilePath, StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    MoveFile(param.FilePath, oldFilePath);
                }
            }

            return result;
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

        private ResultInfo CheckFileInFolder(InsertUpdateBannerParam param, string oldFilePath)
        {
            var folder = BannerHelper.GeneralFolder(param.Category, param.SubCategory);
            param.FilePath = folder + param.InputFileName;
            if (!string.IsNullOrEmpty(param.SubCategory))
            {
                param.Category = param.SubCategory;
            }

            var result = _bannerValidationService.CheckFileInFolder(folder, param.FilePath);

            if (result.Code == Code.ErrTheFileNameAlreadyExists && string.Compare(param.FilePath, oldFilePath, StringComparison.CurrentCultureIgnoreCase) == 0)
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
