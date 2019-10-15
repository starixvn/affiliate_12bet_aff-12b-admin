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
            if ((string.Compare(param.Category, Lang.Casino, StringComparison.CurrentCultureIgnoreCase) == 0
                || string.Compare(param.Category, Lang.Sportsbook, StringComparison.CurrentCultureIgnoreCase) == 0)
                && !string.IsNullOrEmpty(param.SubCategory))
            {
                param.Category = param.SubCategory;
            }

            return _bannerListRepository.GetBannerList(param);
        }

        public BannerInfo GetBannerDetail(int selectedBannerId)
        {
            return _bannerListRepository.GetBannerDetail(selectedBannerId);
        }

        public ResultInfo InsertBanner(HttpPostedFileBase file, InsertUpdateBannerParam param)
        {
            var fileName = file.FileName;
            var result = _bannerValidationService.ValidateFile(fileName, file.ContentLength);

            if (!result.IsSuccessful)
            {
                return result;
            }

            if (string.IsNullOrEmpty(param.InputFileName))
            {
                param.InputFileName = fileName;
            }
            else
            {
                if (!BannerHelper.IsValidExtension(_systemFile.GetExtension(param.InputFileName)))
                {
                    return BannerHelper.CreateErrorResult();
                }
            }

            var folder = BannerHelper.GeneralFolder(param.Category, param.SubCategory);
            var filePath = folder + param.InputFileName;

            result = _bannerValidationService.CheckFileInFolder(folder, filePath);

            if (result.IsSuccessful)
            {
                using (var bitmap = new Bitmap(file.InputStream))
                {
                    param.Size = BannerHelper.GetSize(bitmap.Width, bitmap.Height);
                    param.FilePath = filePath;
                    if (!string.IsNullOrEmpty(param.SubCategory))
                    {
                        param.Category = param.SubCategory;
                    }

                    result = _bannerListRepository.InsertBanner(param);

                    if (result.IsSuccessful)
                    {
                        uploadFile(bitmap, filePath);
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
            var folder = BannerHelper.GeneralFolder(param.Category, param.SubCategory);
            var filePath = folder + param.InputFileName;
            param.FilePath = filePath;
            if (!string.IsNullOrEmpty(param.SubCategory))
            {
                param.Category = param.SubCategory;
            }

            var result = _bannerValidationService.CheckFileInFolder(folder, filePath);
            if (!result.IsSuccessful)
            {
                if (string.Compare(filePath, oldFilePath, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    param.Size = oldBannerInfo.Size;
                    result = _bannerListRepository.UpdateBanner(param);
                }

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

                        uploadFile(bitmap, filePath);
                        result = _bannerListRepository.UpdateBanner(param);

                        DeleteFile(result.IsSuccessful ? oldFilePath : filePath);
                    }
                }
            }
            else
            {
                param.Size = oldBannerInfo.Size;
                result = _bannerListRepository.UpdateBanner(param);

                if (result.IsSuccessful)
                {
                    MoveFile(filePath, oldFilePath);
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

        private void uploadFile(Bitmap bitmap, string fileName)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.UploadFile(bitmap, serverPath + fileName);
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
    }
}
