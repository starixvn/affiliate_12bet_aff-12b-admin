using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services
{
    public class SignInServiceTest
    {
        private IAuthRepository authRepository;
        public SignInServiceTest()
        {
            authRepository = Substitute.For<IAuthRepository>();
        }

        [Fact]
        public void ReCheckin_WhenCall_Excute()
        {
            // Arrange
            var service = CreateSignInService;

            // Action
            service.IsKickOut(123);

            // Assert
            authRepository.Received().ReCheckin(123);
        }

        [Fact]
        public void ReleaseTracking_WhenCall_Excute()
        {
            // Arrange
            var service = CreateSignInService;

            // Action
            service.ReleaseTracking(123);

            // Assert
            authRepository.Received().Logout(123);
        }

        [Fact]
        public void VerifyAccount_WhenCall_Excute()
        {
            // Arrange
            var param = new LoginParam();
            var service = CreateSignInService;

            // Action
            service.VerifyAccount(param);

            // Assert
            authRepository.Received().Login(param);
        }

        [Fact]
        public void TrackLoginInfo_WhenCall_Excute()
        {
            // Arrange
            var param = new LoginParam();
            var service = CreateSignInService;

            // Action
            service.TrackLoginInfo(param, "123", "abc");

            // Assert
            authRepository.Received().LoginOnUser(param, "123", "abc");
        }

        private SignInService CreateSignInService => new SignInService(authRepository);
    }
}
