using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Resources;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class TextListController : BaseController
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
        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.BannerList, Url = Url.Content("~/Portal/MediaGallery") },
                new SubMenu { Name = Lang.TextList, Url = Url.Content("~/Portal/TextList"), IsActive = true },
                new SubMenu { Name = Lang.LandingPageList, Url = Url.Content("~/Portal/LandingPageList") }
            };

            var model = new TextListViewModel
            {
                CategoryList = _commonMediaGalleryService.GetCategoryList(TextListCategory),
                LanguageList = _languageService.GetLanguageList(),
                CurrencyList = _commonMediaGalleryService.GetCurrencyList(),
                SubMenuList = subMenu
            };

            return View("~/Areas/Portal/Views/MediaGallery/TextList.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTextList(TextParam param)
        {
            if (ModelState.IsValid)
            {
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

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
