using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize]
    public class MemberController : BaseController
    {
        private readonly IMemberRepository _memberRepository;

        public MemberController(
            ISessionService sessionService,
            ILanguageService languageService,
            IMemberRepository memberRepository) : base(sessionService, languageService)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var memberParam = new MemberParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                IsGetAll = true,
                MemberType = (byte)MemberType.All,
                PageSize = 20,
                PageIndex = 1
            };

            var model = _memberRepository.GetMemberList(memberParam);
            ViewBag.TotalItem = memberParam.NRows;
            ViewBag.StartRowNumber = 1;

            return View("~/Views/UserPanel/Member/Index.cshtml", model);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMemberList(MemberParam memberParam)
        {
            memberParam.AffId = SessionService.UserSession.AffId;
            memberParam.IsInternal = SessionService.IsInternal;

            var memberList = _memberRepository.GetMemberList(memberParam);
            ViewBag.TotalItem = memberParam.NRows;
            ViewBag.StartRowNumber = (memberParam.PageIndex - 1) * memberParam.PageSize + 1;

            return PartialView("~/Views/UserPanel/Member/_DataContent.cshtml", memberList);
        }
    }
}