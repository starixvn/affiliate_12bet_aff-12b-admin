using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class TextListController : ReadOnlySessionBaseController
    {
        private const int TextListCategory = 2;
        private readonly ILanguageService _languageService;
        private readonly ICommonMediaGalleryService _commonMediaGalleryService;
        private readonly ITextListService _textListService;

        public TextListController(
            ISessionService sessionService,
            ILanguageService languageService,
            ICommonMediaGalleryService commonMediaGalleryService,
            ITextListService textListService) : base(sessionService)
        {
            _languageService = languageService;
            _commonMediaGalleryService = commonMediaGalleryService;
            _textListService = textListService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            var model = new TextListViewModel
            {
                CategoryList = _commonMediaGalleryService.GetCategoryList(TextListCategory),
                LanguageList = _languageService.GetInChangeLanguages(),
                CurrencyList = _commonMediaGalleryService.GetCurrencyList(),
            };

            return View("~/Areas/Portal/Views/MediaGallery/TextList.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTextList(GetTextLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var param = new TextParam
                {
                    Category = model.Category,
                    PageSize = model.PageSize,
                    PageIndex = model.PageIndex,
                    Currency = model.Currency,
                    DeviceType = model.DeviceType,
                    Languages = BuildQueryLanguages(model.Language)
                };

                return new JsonNetResult
                {
                    Data = _textListService.GetTextList(param)
                };
            }

            return new JsonNetResult
            {
                Data = new EntityResult<TextInfo>
                {
                    MainData = new List<TextInfo>(),
                    TotalItem = 0
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTextDetail(int selectedTextId)
        {
            return new JsonNetResult
            {
                Data = _textListService.GetTextDetail(selectedTextId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult InsertText(InsertUpdateTextParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _textListService.InsertText(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Please input text to upload."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateText(InsertUpdateTextParam param)
        {
            var isValidTextId = param.TextId >= Validation.MinId;
            if (ModelState.IsValid && isValidTextId)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _textListService.UpdateText(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = !isValidTextId ? "TextId is invalid." : "Please input text to update."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult DeleteTexts(DeleteTextParam param)
        {
            if (ModelState.IsValid)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _textListService.DeleteTexts(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Please select texts to delete."
                }
            };
        }

        private InsertUpdateTextParam EncodeParam(InsertUpdateTextParam param)
        {
            return new InsertUpdateTextParam
            {
                TextId = param.TextId,
                TextContent = Server.HtmlEncode(param.TextContent),
                Category = param.Category,
                Language = param.Language,
                Currency = param.Currency,
                DeviceType = param.DeviceType
            };
        }

        private string BuildQueryLanguages(string lang = null)
        {
            var langs = _languageService.GetInChangeLanguages();
            if (!String.IsNullOrWhiteSpace(lang))
            {
                langs = langs.Where(l => l.LanguageName.Equals(lang, StringComparison.OrdinalIgnoreCase));
            }

            return langs.JoinStrings(",", l => l.LanguageName);
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
