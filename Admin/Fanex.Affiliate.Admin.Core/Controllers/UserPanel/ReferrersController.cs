using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class ReferrersController : BaseController
    {
        private readonly IReferrersRepository _referrersRepository;
        public ReferrersController(
            ISessionService sessionService,
            IReferrersRepository referrersRepository) : base(sessionService)
        {
            _referrersRepository = referrersRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetReferrersList(ReferrersParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _referrersRepository.GetReferrersList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetReferrersDetailList(ReferrersDetailParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _referrersRepository.GetReferrersDetailList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
