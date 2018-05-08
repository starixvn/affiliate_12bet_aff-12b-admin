namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;

    public partial class UserPanelController
    {
        [HttpGet]
        public ActionResult AccountOverView()
        {
            var userId = 1264888;
            var month = new DateTime(2017, 9, 1);

            var statisticsData = _accountOverviewRepository.GetStatistics(userId, month);

            return View(statisticsData);
        }
    }
}