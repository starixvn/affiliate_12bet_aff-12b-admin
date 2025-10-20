using Fanex.Affiliate.Admin.Core.Commands;
using Fanex.Affiliate.Admin.Core.MetaModels.ChangeAdminPassword;
using Fanex.Affiliate.Admin.Core.Services.ChangeAdminPassword;
using Fanex.Data.Repository;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.ChangeAdminPassword
{
    public class ChangeAdminPasswordServiceTest
    {
        [Fact]
        public void ChangePassword_WhenCall_ChangePasswordIsSuccessful()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<ChangeAdminPasswordCommand>(), It.IsAny<string>()));

            // Action
            var service = new ChangeAdminPasswordService(dynamicRepository.Object);
            var result = service.ChangePassword(new ChangeAdminPasswordParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<ChangeAdminPasswordCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }
    }
}
