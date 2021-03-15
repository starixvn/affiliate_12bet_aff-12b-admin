using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu)]
    public class NewsLetterController : ReadOnlySessionBaseController
    {
        private ILanguageService LanguageService { get; }
        private INewsLetterService NewsLetterService { get; }
        private IAppSetting AppSetting { get; }

        public NewsLetterController(INewsLetterService newsLetterService, ISessionService sessionService, ILanguageService languageService, IAppSetting appSetting) : base(sessionService)
        {
            LanguageService = languageService;
            NewsLetterService = newsLetterService;
            AppSetting = appSetting;
        }

        public ActionResult Index()
        {
            ViewBag.PreviewUrl = $"{AppSetting.AsianAffSiteUrl}/UserPanel/NewsLetter/Preview";
            ViewBag.Languages = LanguageService.GetLanguageList();
            return View("~/Areas/Portal/Views/MediaGallery/NewsLetterPageList.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult InsertNewsLetter(HttpPostedFileBase file, NewsLetterViewModel model)
        {
            if (ModelState.IsValid && file != null)
            {
                var param = new InsertNewsLetterParam
                {
                    UserId = SessionService.UserId,
                    Ip = SessionService.RequestIp,
                    EffectiveFrom = model.FromDate,
                    EffectiveTo = model.ToDate,
                    FileName = model.FileName,
                    FileSize = file.ContentLength,
                    Language = model.Language
                };

                return new JsonNetResult
                {
                    Data = NewsLetterService.AddNewsLetter(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidFileBrowser
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult UpdateNewsLetter(HttpPostedFileBase file, NewsLetterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var param = new UpdateNewsLetterParam
                {
                    NewsLetterId = model.NewsLetterId,
                    UserId = SessionService.UserId,
                    Ip = SessionService.RequestIp,
                    EffectiveFrom = model.FromDate,
                    EffectiveTo = model.ToDate,
                    FileName = model.FileName,
                    FileSize = file?.ContentLength ?? model.FileSize,
                    Language = model.Language
                };

                return new JsonNetResult
                {
                    Data = NewsLetterService.UpdateNewsLetter(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidFileBrowser
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNewsLetterList(SelectNewsLetterParam param)
        {
            var data = NewsLetterService.GetNewsLetters(param);
            var models = data.MainData.Select(i => new NewsLetterViewModel
            {
                FileName = i.FileName,
                FromDate = i.EffectiveFromDate,
                Language = i.Language,
                NewsLetterId = i.NewsLetterId,
                ToDate = i.EffectiveToDate,
                UploadDate = i.UploadDate,
                FileSize = i.FileSize,
                LanguageName = i.LanguageName
            });
            return new JsonNetResult
            {
                Data = new EntityResult<NewsLetterViewModel>
                {
                    MainData = models,
                    TotalItem = data.TotalItem
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult DeleteNewsLetters(string ids)
        {
            var param = new DeleteNewsLetterParam
            {
                NewsLetterIds = ids,
                UserId = SessionService.UserId,
                Ip = SessionService.RequestIp,

            };

            return new JsonNetResult
            {
                Data = NewsLetterService.DeleteNewsLetters(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNewsLetterDetailById(int id)
        {
            var item = NewsLetterService.GetNewsLetterById(id);
            var model = new NewsLetterViewModel
            {
                FileName = item.FileName,
                FromDate = item.EffectiveFromDate,
                ToDate = item.EffectiveToDate,
                Language = item.Language,
                NewsLetterId = item.NewsLetterId,
                UploadDate = item.UploadDate,
                FileSize = item.FileSize
            };
            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
