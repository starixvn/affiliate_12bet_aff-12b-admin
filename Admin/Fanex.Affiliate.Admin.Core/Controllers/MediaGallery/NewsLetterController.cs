using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu)]
    public class NewsLetterController : ReadOnlySessionBaseController
    {
        private ISystemService CommonSettingService { get; }
        private INewsLetterService NewsLetterService { get; }
        private IAppSetting AppSetting { get; }

        public NewsLetterController(INewsLetterService newsLetterService, ISessionService sessionService, ISystemService commonSettingService, IAppSetting appSetting) : base(sessionService)
        {
            CommonSettingService = commonSettingService;
            NewsLetterService = newsLetterService;
            AppSetting = appSetting;
        }

        public ActionResult Index()
        {
            ViewBag.PreviewUrl = $"{AppSetting.AsianAffSiteUrl}/UserPanel/NewsLetter/Preview";
            ViewBag.Languages = CommonSettingService.GetInChangeMarkets();
            return View("~/Areas/Portal/Views/MediaGallery/Common.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MediaGalleryMenu, IsHost = true)]
        public ActionResult InsertNewsLetter(HttpPostedFileBase file, NewsLetterViewModel model)
        {
            if (file == null)
            {
                return new JsonNetResult
                {
                    Data = ResultInfo.GenerateErr(Lang.InvalidFileBrowser)
                };
            }

            if (ModelState.IsValid)
            {
                var param = new InsertHtmlFileParam
                {
                    UserId = SessionService.UserId,
                    Ip = SessionService.RequestIp,
                    EffectiveFrom = model.FromDate,
                    EffectiveTo = model.ToDate,
                    FileName = Server.HtmlEncode(model.FileName),
                    FileSize = file.ContentLength,
                    Language = Server.HtmlEncode(model.Language)
                };

                return new JsonNetResult
                {
                    Data = NewsLetterService.AddNewsLetter(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("<br />", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)))
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
                    FileName = Server.HtmlEncode(model.FileName),
                    FileSize = file?.ContentLength ?? model.FileSize,
                    Language = Server.HtmlEncode(model.Language)
                };

                return new JsonNetResult
                {
                    Data = NewsLetterService.UpdateNewsLetter(file, param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("<br />", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)))
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNewsLetterList(PagingParam param)
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
