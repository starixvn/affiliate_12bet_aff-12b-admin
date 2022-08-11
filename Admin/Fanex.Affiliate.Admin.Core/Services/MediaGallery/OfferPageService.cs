using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class OfferPageService : IOfferPageService
    {
        private const string UPLOAD_FOLDER = "CategoryMedia\\OfferPage";

        private readonly IHtmlFileService _htmlFileService;
        private readonly IOfferPageRepository _offerPageRepository;
        private readonly IDateTimeHelper _dateTimeHelper;

        public OfferPageService(
            IHtmlFileService htmlFileService,
            IOfferPageRepository offerPageRepository,
            IDateTimeHelper dateTimeHelper)
        {
            _htmlFileService = htmlFileService;
            _offerPageRepository = offerPageRepository;
            _dateTimeHelper = dateTimeHelper;
        }

        public OfferPage GetOfferPageById(int pageId) => _offerPageRepository.GetOfferPageById(pageId);

        public EntityResult<OfferPage> GetOfferPageList(PagingParam param) => _offerPageRepository.GetOfferPageList(param);

        public IEnumerable<OfferPageBasicInfo> GetValidEndDateOfferPages()
        {
            var offerPages = _offerPageRepository.GetOfferPageList(new PagingParam()).MainData;

            return offerPages.Where(o => DateTime.Compare(o.EffectiveToDate, _dateTimeHelper.GetNowDateTime()) > 0)
                .Select(o => new OfferPageBasicInfo
                {
                    PageId = o.PageId,
                    FileName = o.FileName
                });
        }

        public ResultInfo DeleteOfferPages(DeleteOfferPageParam param)
        {
            var fileNames = GetFileNamesFromIds(param.PageIds);
            var result = _offerPageRepository.DeleteOfferPage(param);

            if (result.IsSuccessful)
            {
                _htmlFileService.DeleteFiles(fileNames, UPLOAD_FOLDER);
            }

            return result;
        }

        public ResultInfo InsertOfferPage(HttpPostedFileBase file, InsertHtmlFileParam param)
        {
            var result = _htmlFileService.HandleUploadFile(file, param.FileName, UPLOAD_FOLDER);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = InsertToDB(param);

            if (!result.IsSuccessful)
            {
                _htmlFileService.DeleteFile(param.FileName, UPLOAD_FOLDER);
            }

            return result;
        }

        public ResultInfo UpdateOfferPage(HttpPostedFileBase file, UpdateOfferPageParam param)
        {
            var result = _htmlFileService.ValidateFileName(param.FileName);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var oldOfferPage = _offerPageRepository.GetOfferPageById(param.PageId);
            if (oldOfferPage == null)
            {
                return ResultInfo.GenerateErr(Lang.InvalidId);
            }

            result = _htmlFileService.HandleUpdateFile(file, oldOfferPage.FileName, param.FileName, UPLOAD_FOLDER);

            if (!result.IsSuccessful)
            {
                return result;
            }

            return _offerPageRepository.UpdateOfferPage(param);
        }

        private ResultInfo InsertToDB(InsertHtmlFileParam param)
        {
            try
            {
                return _offerPageRepository.InsertOfferPage(param);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                return ResultInfo.GenerateErr(Lang.ErrGeneral);
            }
        }

        private IEnumerable<string> GetFileNamesFromIds(string pageIds)
        {
            return pageIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Convert.ToInt32(i))
                .Select(i => GetOfferPageById(i))
                .Select(item => item?.FileName)
                .ToList();
        }
    }
}
