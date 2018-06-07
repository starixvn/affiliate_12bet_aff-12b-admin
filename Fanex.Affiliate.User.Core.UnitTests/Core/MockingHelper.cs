using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Moq;

namespace Fanex.Affiliate.User.Core.UnitTests.Core
{
    public static class MockingHelper
    {
        public static ISessionService GetSessionService()
        {
            return Mock.Of<ISessionService>(ss =>
                ss.CaptchaCode == "1234" &&
                ss.CurrentLanguage == Constant.DefaultLanguage &&
                !ss.IsInternal &&
                ss.UserSession == new UserSession
                {
                    AffId = Constant.DefaultAffId,
                    AffType = Constant.AffTypeAsian,
                    OnUserId = 1,
                    PreferredLanguage = Constant.DefaultLanguage,
                    UserName = Constant.DefaultUserName
                }
            );
        }

        public static UserPanelController GetUserPanelController(
            ISessionService sessionService = null,
            IAppSetting appSetting = null,
            IAccountRepository accountRepository = null,
            IMarketingService marketingService = null,
            IStatisticsRepository statisticsRepository = null,
            IStatementHistoryRepository statementHistoryRepository = null,
            ICommissionRepository commissionRepository = null)
        {
            return new UserPanelController(
                sessionService ?? GetSessionService(),
                appSetting ?? Mock.Of<IAppSetting>(),
                accountRepository ?? Mock.Of<IAccountRepository>(),
                marketingService ?? Mock.Of<IMarketingService>(),
                statisticsRepository ?? Mock.Of<IStatisticsRepository>(),
                statementHistoryRepository ?? Mock.Of<IStatementHistoryRepository>(),
                commissionRepository ?? Mock.Of<ICommissionRepository>());
        }
    }
}