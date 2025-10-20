using Fanex.Affiliate.Admin.Core.Commands.UserPanel.Adjustment;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Data.Repository;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class AdjustmentServiceTest
    {
        [Fact]
        public void GetBalanceBroughtForward_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<BalanceBroughtForwardCommand>(), It.IsAny<string>()));

            // Action
            var service = new AdjustmentService(dynamicRepository.Object);
            var result = service.GetBalanceBroughtForward(123);

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<BalanceBroughtForwardCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Equal(0, result);
        }

        [Fact]
        public void InsertAdjustment_WhenCallWithInvalidParam_InsertIsFailed()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<InsertAdjustmentCommand>(), It.IsAny<string>()));

            // Action
            var service = new AdjustmentService(dynamicRepository.Object);
            var result = service.InsertAdjustment(new AdjustmentParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<InsertAdjustmentCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }
    }
}
