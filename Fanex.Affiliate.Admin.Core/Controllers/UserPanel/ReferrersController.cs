using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;

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

            return View("~/Areas/Portal/Views/UserPanel/Referrers/Index.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetReferrersAllSites(ReferrersParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _referrersRepository.GetReferrersAllSites(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetReferrersBySite(ReferrersDetailParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _referrersRepository.GetReferrersBySite(param);

            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
