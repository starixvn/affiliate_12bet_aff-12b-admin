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
    public class NewsLetterService : INewsLetterService
    {
        private const string UPLOAD_FOLDER = "CategoryMedia\\NewsLetter";

        private INewsLetterRepository Repo { get; }
        private readonly IHtmlFileService _htmlFileService;

        public NewsLetterService(INewsLetterRepository repo, IHtmlFileService htmlFileService)
        {
            Repo = repo;
            _htmlFileService = htmlFileService;
        }

        public EntityResult<NewsLetter> GetNewsLetters(PagingParam param)
            => Repo.GetNewsLetters(param);

        public NewsLetter GetNewsLetterById(int id)
            => Repo.GetNewsLetterById(id);

        public ResultInfo AddNewsLetter(HttpPostedFileBase file, InsertHtmlFileParam param)
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

        public ResultInfo UpdateNewsLetter(HttpPostedFileBase file, UpdateNewsLetterParam param)
        {
            var result = _htmlFileService.ValidateFileName(param.FileName);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var oldNewsLetter = Repo.GetNewsLetterById(param.NewsLetterId);
            if (oldNewsLetter == null)
            {
                return ResultInfo.GenerateErr(Lang.InvalidId);
            }

            result = _htmlFileService.HandleUpdateFile(file, oldNewsLetter.FileName, param.FileName, UPLOAD_FOLDER);

            if (!result.IsSuccessful)
            {
                return result;
            }

            return Repo.UpdateNewsLetter(param);
        }

        public ResultInfo DeleteNewsLetters(DeleteNewsLetterParam param)
        {
            var fileNames = GetFileNamesFromIds(param.NewsLetterIds);
            var result = Repo.DeleteNewsLetter(param);

            if (result.IsSuccessful)
            {
                _htmlFileService.DeleteFiles(fileNames, UPLOAD_FOLDER);
            }
            return result;
        }

        private IEnumerable<string> GetFileNamesFromIds(string newsLetterIds)
        {
            return newsLetterIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Convert.ToInt32(i))
                .Select(i => GetNewsLetterById(i))
                .Select(item => item?.FileName)
                .ToList();
        }

        private ResultInfo InsertToDB(InsertHtmlFileParam param)
        {
            try
            {
                return Repo.InsertNewsLetter(param);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                return ResultInfo.GenerateErr(Lang.ErrGeneral);
            }
        }
    }
}
