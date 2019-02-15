using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;

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
        public ActionResult Index(int affId, string affName, string fromDate, string toDate, int deviceType = 0, int current = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Referrers,
                    Url = Url.Content($"~/Portal/UserPanel/Referrers/Index?affId={(affId == 0 ? SessionService.AffId : affId)}" + 
                                      $"&affName={affName}&fromDate={fromDate}&toDate={toDate}&deviceType={deviceType}") },
                new SubMenu { Name = Lang.LandingPages,
                    Url = Url.Content($"~/Portal/UserPanel/Referrers/Index?affId={(affId == 0 ? SessionService.AffId : affId)}&affName={affName}&current=1") }
            };

            var activeIndex = current > subMenu.Count - 1 ? 0 : current;
            subMenu[activeIndex].IsActive = true;

            return View("~/Areas/Portal/Views/UserPanel/Referrers/Index.cshtml", subMenu);
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
