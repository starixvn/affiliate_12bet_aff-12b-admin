using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.EmailHistory;
using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Services.EmailHistory;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.EmailHistory
{
    public class EmailHistoryServiceTest
    {
        [Fact]
        public void DeleteEmailHistory_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var param = new DeleteEmailHistoryParam { EmailIdList = "123" };
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<DeleteEmailHistoryCommand>(), It.IsAny<string>()));

            var service = CreateEmailHistoryService(repository.Object);

            // Action
            var result = service.DeleteEmailHistory(param);

            // Assert
            repository.Verify(d => d.Execute(It.Is<DeleteEmailHistoryCommand>(c =>
                c.EmailIdList == param.EmailIdList &&
                c.GetSettingKey() == "Aff_Admin_EmailHistory_Del" &&
                c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetEmailHistoryDetail_WhenCallWithValidId_ReturnCorrectData()
        {
            // Arrange
            var emailId = 123;
            var expectedDetail = new EmailHistoryDetailInfo { EmailId = emailId };
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Get<EmailHistoryDetailInfo>(It.IsAny<EmailHistoryDetailCriteria>(), It.IsAny<string>()))
                .Returns(expectedDetail);

            var service = CreateEmailHistoryService(repository.Object);

            // Action
            var result = service.GetEmailHistoryDetail(emailId);

            // Assert
            repository.Verify(d => d.Get<EmailHistoryDetailInfo>(
                It.Is<EmailHistoryDetailCriteria>(c =>
                    c.MID == emailId &&
                    !string.IsNullOrEmpty(c.GetSettingKey()) &&
                    c.IsValid()),
                It.IsAny<string>()));
            Assert.NotNull(result);
            Assert.Equal(emailId, result.EmailId);
        }

        [Fact]
        public void GetEmailHistoryDetail_WhenCallWithInvalidId_ReturnNull()
        {
            // Arrange
            var emailId = 999;
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Get<EmailHistoryDetailInfo>(It.IsAny<EmailHistoryDetailCriteria>(), It.IsAny<string>()))
                .Returns(null as EmailHistoryDetailInfo);

            var service = CreateEmailHistoryService(repository.Object);

            // Action
            var result = service.GetEmailHistoryDetail(emailId);

            // Assert
            repository.Verify(d => d.Get<EmailHistoryDetailInfo>(
                It.Is<EmailHistoryDetailCriteria>(c =>
                    c.MID == emailId &&
                    !string.IsNullOrEmpty(c.GetSettingKey()) &&
                    c.IsValid()),
                It.IsAny<string>()));
            Assert.Null(result);
        }

        [Fact]
        public void GetEmailHistoryList_WhenCallWithValidParam_ReturnCorrectData()
        {
            // Arrange
            var param = new EmailHistoryParam();
            var expectedList = new List<EmailHistoryInfo> { new EmailHistoryInfo() };
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<EmailHistoryInfo>(It.IsAny<EmailHistoryCriteria>(), It.IsAny<string>()))
                .Returns(expectedList);

            var service = CreateEmailHistoryService(repository.Object);

            // Action
            var result = service.GetEmailHistoryList(param);

            // Assert
            repository.Verify(d => d.Fetch<EmailHistoryInfo>(
                It.Is<EmailHistoryCriteria>(c => c.IsValid()),
                It.IsAny<string>()));
            Assert.NotNull(result);
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetEmailHistoryList_WhenCallWithEmptyResult_ReturnEmptyList()
        {
            // Arrange
            var param = new EmailHistoryParam();
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<EmailHistoryInfo>(It.IsAny<EmailHistoryCriteria>(), It.IsAny<string>()))
                .Returns(new List<EmailHistoryInfo>());

            var service = CreateEmailHistoryService(repository.Object);

            // Action
            var result = service.GetEmailHistoryList(param);

            // Assert
            repository.Verify(d => d.Fetch<EmailHistoryInfo>(
                It.Is<EmailHistoryCriteria>(c => c.IsValid()),
                It.IsAny<string>()));
            Assert.NotNull(result);
            Assert.Empty(result.MainData);
        }

        private static EmailHistoryService CreateEmailHistoryService(IDynamicRepository repository = null)
        {
            var fakeRepository = repository ?? Mock.Of<IDynamicRepository>();
            return new EmailHistoryService(fakeRepository);
        }
    }
}
