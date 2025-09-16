namespace Fanex.Affiliate.Admin.Core.Controllers.CampaignManagement
{
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using MetaModels.CampaignManagement;
    using Services.CampaignManagement;
    using Services.Common;

    public class CampaignManagementController : ReadOnlySessionBaseController
    {
        private readonly ICampaignService _campaignService;

        public CampaignManagementController(
            ICampaignService campaignService,
            ISessionService sessionService) : base(sessionService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AllCampaigns)]
        public ActionResult GetDashboard()
        {
            var result = _campaignService.GetCampaignDashboard();
            return GenerateJsonNetResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AllCampaigns)]
        public ActionResult GetAllCampaignStatus()
        {
            var result = _campaignService.GetAllCampaignStatus();
            return GenerateJsonNetResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AllCampaigns)]
        public ActionResult GetCampaignManagementList(GetCampaignListParam param)
        {
            var result = _campaignService.GetCampaignList(param);

            return GenerateJsonNetResult(result);
        }
    }
}
