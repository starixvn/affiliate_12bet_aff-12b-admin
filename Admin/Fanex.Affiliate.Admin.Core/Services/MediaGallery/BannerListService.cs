using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using System.IO;
using Fanex.Affiliate.Admin.Core.Utilities.MediaGallery;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class BannerListService : IBannerListService
    {
        private readonly IBannerListRepository _bannerListRepository;
        public BannerListService(IBannerListRepository bannerListRepository)
        {
            _bannerListRepository = bannerListRepository;
        }

        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            if ((string.Compare(param.Category, "Casino", StringComparison.CurrentCultureIgnoreCase) == 0
                || string.Compare(param.Category, "Sportsbook", StringComparison.CurrentCultureIgnoreCase) == 0) && string.IsNullOrEmpty(param.SubCategory))
            {
                return _bannerListRepository.GetBannerList(param, "Aff_BannerSelByParentCate");
            }

            if (!string.IsNullOrEmpty(param.SubCategory))
            {
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
            var extension = string.Empty;
            if (!string.IsNullOrEmpty(param.FileName))
            {
                extension = Path.GetExtension(param.FileName);
                if (!BannerHelper.IsValidExtension(extension))
                {
                    return CreateErrorResult();
                }
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

            var newFolder = string.Empty;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("CategoryMedia/");
            stringBuilder.Append(param.Category);
            stringBuilder.Append("/");
            if (string.Compare(param.Category, "Casino", StringComparison.CurrentCultureIgnoreCase) == 0
                || string.Compare(param.Category, "Sportsbook", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                stringBuilder.Append(param.SubCategory);
                stringBuilder.Append("/");
                param.Category = param.SubCategory;
            }
            newFolder = stringBuilder.ToString();
            stringBuilder.Append(param.InputFileName);
            param.FileName = stringBuilder.ToString();

            var result = BannerHelper.UploadFileWithAutoDetectSize(param, newFolder);

            if (result.IsSuccessful)
            {
                result = _bannerListRepository.InsertBanner(param);
                if (!result.IsSuccessful)
                {
                    BannerHelper.DeleteFile(Path.Combine(newFolder, param.InputFileName));
                }
            }

            if (result.IsSuccessful)
            {
                result.Message = "Your banner has been added successfully!.";
            }

            return result;
        }

        public ResultInfo UpdateBanner(InsertUpdateBannerParam param)
        {
            return _bannerListRepository.UpdateBanner(param);
        }

        public ResultInfo DeleteBanners(DeleteBannerParam param)
        {
            return _bannerListRepository.DeleteBanners(param);
        }

        private static ResultInfo CreateErrorResult()
        {
            return new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = "Sorry! We just support these file types : .jpg, .gif, .png, .swf, .jpeg"
            };
        }
    }
}
