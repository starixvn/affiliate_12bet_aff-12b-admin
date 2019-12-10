using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.MediaGallery
{
    [FunctionAuthorize(FunctionName = Function.Media)]
    public class LandingPageListController : BaseController
    {
        private readonly ILandingPageService _landingPageService;

        public LandingPageListController(ISessionService sessionService, ILandingPageService landingPageService) : base(sessionService)
        {
            _landingPageService = landingPageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.BannerList, Url = Url.Content("~/Portal/MediaGallery") },
                new SubMenu { Name = Lang.TextList, Url = Url.Content("~/Portal/TextList") },
                new SubMenu { Name = Lang.LandingPageList, Url = Url.Content("~/Portal/LandingPageList"), IsActive = true }
            };

            return View("~/Areas/Portal/Views/MediaGallery/LandingPageList.cshtml", subMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageList()
        {
            return new JsonNetResult
            {
                Data = _landingPageService.GetLandingPageList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageItem(int pageId)
        {
            return new JsonNetResult
            {
                Data = _landingPageService.GetLandingPageById(pageId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                AssignCommonParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _landingPageService.InsertLandingPage(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                AssignCommonParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _landingPageService.UpdateLandingPage(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult DeleteLandingPage(DeleteLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _landingPageService.DeleteLandingPage(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        private InsertUpdateLandingPageParam EncodeParam(InsertUpdateLandingPageParam param)
        {
            return new InsertUpdateLandingPageParam
            {
                PageId = param.PageId,
                PageName = Server.HtmlEncode(param.PageName),
                PageCode = Server.HtmlEncode(param.PageCode)
            };
        }

        private void AssignCommonParam(InsertUpdateLandingPageParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}