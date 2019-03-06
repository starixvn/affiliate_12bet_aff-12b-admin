using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class ReferrersController : BaseController
    {
        private readonly IReferrersRepository _referrersRepository;
        public ReferrersController(
            ISessionService sessionService,
            IReferrersRepository referrersRepository) : base(sessionService)
        {
            _referrersRepository = referrersRepository;
        }

        [HttpGet]
        public ActionResult Index(ReferrersParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Referrers,
                    Url = Url.Content($"~/Portal/UserPanel/Referrers/Index?affId={(param.AffId)}&affName={param.AffName}") },
                new SubMenu { Name = Lang.LandingPages,
                    Url = Url.Content($"~/Portal/UserPanel/LandingPageStatistics/Index?affId={(param.AffId)}&affName={param.AffName}") }
            };

            subMenu[0].IsActive = true;

            var viewModel = new ReferrersViewModel
            {
                SubMenuList = subMenu,
                ReferrersParam = param
            };

            return View("~/Areas/Portal/Views/UserPanel/Referrers/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetReferrersList(ReferrersParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _referrersRepository.GetReferrersList(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetReferrersDetailList(ReferrersDetailParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _referrersRepository.GetReferrersDetailList(param);

            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
