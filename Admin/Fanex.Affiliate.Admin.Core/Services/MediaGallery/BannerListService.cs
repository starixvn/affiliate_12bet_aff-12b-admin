using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using System.IO;
using Fanex.Affiliate.Admin.Core.Utilities.MediaGallery;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Text;
using Fanex.Affiliate.Admin.Core.Configurations;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class BannerListService : IBannerListService
    {
        private readonly IBannerListRepository _bannerListRepository;
        private readonly IAppSetting _appSetting;
        public BannerListService(IBannerListRepository bannerListRepository, IAppSetting appSetting)
        {
            _bannerListRepository = bannerListRepository;
            _appSetting = appSetting;
        }

        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            var a = string.Compare(param.Category, Lang.Casino, StringComparison.CurrentCultureIgnoreCase);
            if ((string.Compare(param.Category, Lang.Casino, StringComparison.CurrentCultureIgnoreCase) == 0
                || string.Compare(param.Category, Lang.Sportsbook, StringComparison.CurrentCultureIgnoreCase) == 0))
            {
                if (string.IsNullOrEmpty(param.SubCategory))
                {
                    return _bannerListRepository.GetBannerList(param, "Aff_BannerSelByParentCate");
                }

                param.Category = param.SubCategory;
            }

            return _bannerListRepository.GetBannerList(param, "Aff_BannerSel_1");
        }

        public BannerInfo GetBannerDetail(int selectedBannerId)
        {
            return _bannerListRepository.GetBannerDetail(selectedBannerId);
        }

        public ResultInfo InsertBanner(InsertUpdateBannerParam param)
        {
            if (!BannerHelper.IsValidExtension(Path.GetExtension(param.FileName)))
            {
                return CreateErrorResult();
            }

            if (string.IsNullOrEmpty(param.InputFileName))
            {
                param.InputFileName = param.FileName;
            }
            else
            {
                if (!BannerHelper.IsValidExtension(Path.GetExtension(param.InputFileName)))
                {
                    return CreateErrorResult();
                }
            }

            var newFolder = GetNewFolder(param);
            var result = BannerHelper.UploadFileWithAutoDetectSize(_appSetting, param, newFolder);

            if (result.IsSuccessful)
            {
                result = _bannerListRepository.InsertBanner(param);

                if (!result.IsSuccessful)
                {
                    BannerHelper.DeleteFile(_appSetting, param.FileName);
                }
            }

            return result;
        }

        public ResultInfo UpdateBanner(InsertUpdateBannerParam param)
        {
            if (!BannerHelper.IsValidExtension(Path.GetExtension(param.InputFileName)))
            {
                return CreateErrorResult();
            }

            if (!string.IsNullOrEmpty(param.FileName) && !BannerHelper.IsValidExtension(Path.GetExtension(param.FileName)))
            {
                return CreateErrorResult();
            }

            var oldBannerInfo = _bannerListRepository.GetBannerDetail(param.BannerId);
            var oldFilePath = oldBannerInfo.FileName;
            var newFolder = GetNewFolder(param);
            param.Size = oldBannerInfo.Size;

            return param.File != null ? UpdateBannerByUploadNewFile(param, oldFilePath, newFolder) : UpdateBannerByMoveFile(param, oldFilePath, newFolder);
        }

        private ResultInfo ValidateFilePath(string newFolder, string newFilePath)
        {
            var validateFolderResult = BannerHelper.EnsureFoldersExisted(_appSetting, newFolder);

            if (validateFolderResult.IsSuccessful)
            {
                foreach (var folder in _appSetting.BannerFolders)
                {
                    var newFullPath = Path.Combine(folder, newFilePath);
                    if (File.Exists(newFullPath))
                    {
                        return new ResultInfo
                        {
                            Code = Code.GeneralErrorCode,
                            Message = Lang.InvalidBannerAlready
                        };
                    }
                }
            }

            return validateFolderResult;
        }

        private ResultInfo UpdateBannerByMoveFile(InsertUpdateBannerParam param, string oldFilePath, string newFolder)
        {
            var validateFilePathResult = ValidateFilePath(newFolder, param.FileName);

            if (validateFilePathResult.IsSuccessful)
            {
                var result = _bannerListRepository.UpdateBanner(param);

                if (result.IsSuccessful)
                {
                    BannerHelper.MoveFiles(_appSetting, param.FileName, oldFilePath);
                }

                return result;
            }

            return validateFilePathResult;
        }

        private ResultInfo UpdateBannerByUploadNewFile(InsertUpdateBannerParam param, string oldFilePath, string newFolder)
        {
            var uploadingResult = BannerHelper.UploadFileWithAutoDetectSize(_appSetting, param, newFolder);
            if (uploadingResult.IsSuccessful)
            {
                var result = _bannerListRepository.UpdateBanner(param);

                if (!result.IsSuccessful)
                {
                    BannerHelper.DeleteFile(_appSetting, oldFilePath);
                }

                return result;
            }

            return uploadingResult;
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

        private void DeleteFiles(List<string> files)
        {
            for (int i = 0; i < files.Count; i++)
            {
                BannerHelper.DeleteFile(_appSetting, files[i].Replace("/", "\\"));
            }
        }

        private static string GetNewFolder(InsertUpdateBannerParam param)
        {
            var newFolder = string.Empty;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("CategoryMedia/");
            stringBuilder.Append(param.Category);
            stringBuilder.Append("/");
            if (string.Compare(param.Category, Lang.Casino, StringComparison.CurrentCultureIgnoreCase) == 0
                || string.Compare(param.Category, Lang.Sportsbook, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                stringBuilder.Append(param.SubCategory);
                stringBuilder.Append("/");
                param.Category = param.SubCategory;
            }
            newFolder = stringBuilder.ToString();
            stringBuilder.Append(param.InputFileName);
            param.FileName = stringBuilder.ToString();

            return newFolder;
        }
        private static ResultInfo CreateErrorResult()
        {
            return new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = Lang.InvalidBannerExtensionFile
            };
        }
    }
}
