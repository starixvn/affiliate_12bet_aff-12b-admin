using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Repository;
using Moq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class ContactServiceTest
    {
        [Fact]
        public void SendRequest_WhenCall_ReturnCorrectResult()
        {
            // Arrange
            var contactUsParam = new ContactUsParam();
            var emailParam = new EmailBaseParam();

            var resultInfo = new ResultInfo { IsOk = true };
            var repository = new Mock<IEmailHistoryRepository>();
            repository.Setup(re => re.Insert(contactUsParam)).Returns(resultInfo);

            var service = CreateService(repository.Object);

            // Action
            var result = service.SendRequest(contactUsParam, emailParam);

            // Assert
            Assert.Equal(resultInfo, result);
        }

        [Fact]
        public void SendRequest_InsertSuccessfully_SendEmailWithCorrectParam()
        {
            // Arrange
            var contactUsParam = new ContactUsParam();
            var emailParam = new EmailBaseParam();
            var resultInfo = new ResultInfo { IsOk = true };

            var repository = new Mock<IEmailHistoryRepository>();
            repository.Setup(re => re.Insert(contactUsParam)).Returns(resultInfo);

            var emailService = new Mock<IEmailService>();

            var service = CreateService(repository.Object, emailService.Object);

            // Action
            service.SendRequest(contactUsParam, emailParam);

            // Assert
            emailService.Verify(e => e.SendContactUsEmail(emailParam, contactUsParam), Times.Once);
        }

        [Fact]
        public void SendRequest_InsertFailed_DoNotSendEmail()
        {
            // Arrange
            var contactUsParam = new ContactUsParam();
            var resultInfo = new ResultInfo { IsOk = false, Message = "Failed" };

            var repository = new Mock<IEmailHistoryRepository>();
            repository.Setup(re => re.Insert(contactUsParam)).Returns(resultInfo);

            var emailService = new Mock<IEmailService>();

            var service = CreateService(repository.Object, emailService.Object);

            // Action
            service.SendRequest(contactUsParam, new EmailBaseParam());

            // Assert
            emailService.Verify(e => e.SendContactUsEmail(It.IsAny<EmailBaseParam>(), It.IsAny<ContactUsParam>()), Times.Never);
        }

        private static ContactService CreateService(IEmailHistoryRepository repository = null, IEmailService emailService = null)
        {
            repository = repository ?? Mock.Of<IEmailHistoryRepository>();
            emailService = emailService ?? Mock.Of<IEmailService>();

            return new ContactService(repository, emailService);
        }
    }
}
