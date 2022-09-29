
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Controllers.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class ProcessingRequestControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            //Arrange
            var commonSettingService = new Mock<ICommonSettingService>();
            commonSettingService.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language() });

            var controller = CreateProcessingRequestController(commonSettingService: commonSettingService.Object);

            //Action
            var result = controller.Index("Test") as ViewResult;

            //Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/ProcessingRequest.cshtml", result.ViewName);
            Assert.Equal("Test", result.ViewBag.UserName as string);
            Assert.Single(result.ViewBag.Languages as List<Language>);
        }

        [Fact]
        public void GetProcessingRequestList_WhenCallValidParam_ReturnCorrectData()
        {
            //Arrange
            var commonSettingService = new Mock<ICommonSettingService>();
            commonSettingService.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language() });
            var processingRequestService = new Mock<IProcessingRequestService>();
            processingRequestService.Setup(l => l.GetProcessingRequestList(It.IsAny<ProcessingRequestSelParam>())).Returns(new ProcessingRequestEntity { TotalItem = 0 });

            var controller = CreateProcessingRequestController(processingRequestService.Object, commonSettingService.Object);

            //Action
            var result = controller.GetProcessingRequestList(new ProcessingRequestSelParam()) as JsonNetResult;

            //Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetProcessingRequestList_WhenCallInvalidParam_ReturnNull()
        {
            //Arrange
            var controller = CreateProcessingRequestController();
            controller.ModelState.AddModelError("Error", "Invalid");

            //Action
            var result = controller.GetProcessingRequestList(new ProcessingRequestSelParam()) as JsonNetResult;

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetProcessingRequestDetail_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var processingRequestService = new Mock<IProcessingRequestService>();
            processingRequestService.Setup(l => l.GetProcessingRequestDetail(It.IsAny<int>())).Returns(new ProcessingRequestDetailInfo());

            var controller = CreateProcessingRequestController(processingRequestService.Object);

            //Action
            var result = controller.GetProcessingRequestDetail(123) as JsonNetResult;

            //Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void DeleteProcessingRequest_WhenCall_DeleteSuccesfully()
        {
            //Arrange
            var processingRequestService = new Mock<IProcessingRequestService>();
            processingRequestService.Setup(l => l.DeleteProcessingRequest(It.IsAny<ProcessingRequestDelParam>())).Returns(new ResultInfo());

            var controller = CreateProcessingRequestController(processingRequestService.Object);

            //Action
            var result = controller.DeleteProcessingRequest(new ProcessingRequestDelParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            //Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void InsertProcessingRequest_WhenCallValidParam_InsertSuccessfully()
        {
            //Arrange
            var processingRequestService = new Mock<IProcessingRequestService>();
            processingRequestService.Setup(l => l.AddProcessingRequest(It.IsAny<ProcessingRequestInsParam>())).Returns(new ResultInfo());

            var controller = CreateProcessingRequestController(processingRequestService.Object);

            //Action
            var result = controller.InsertProcessingRequest(new ProcessingRequestInsParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            //Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void InsertProcessingRequest_WhenCallInvalidParam_ReturnError()
        {
            //Arrange
            var controller = CreateProcessingRequestController();
            controller.ModelState.AddModelError("Error", "Invalid");

            //Action
            var result = controller.InsertProcessingRequest(new ProcessingRequestInsParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            //Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Invalid", resultInfo.Message);
        }

        [Theory]
        [InlineData(Function.ProcessingRequestManagementRole)]
        [InlineData(Function.ProcessingRequestMarketingRole)]
        public void UpdateProcessingRequest_WhenCallValidParam_ReturnCorrectData(string permissionName)
        {
            //Arrange
            var sessionService = MockingHelper.GetSessionService(permissionName, true, true);
            var processingRequestService = new Mock<IProcessingRequestService>();
            processingRequestService.Setup(l => l.GetProcessingRequestDetail(It.IsAny<int>())).Returns(new ProcessingRequestDetailInfo{ Status = ProcessingRequestStatus.DRAFT });
            processingRequestService.Setup(l => l.UpdateProcessingRequest(It.IsAny<ProcessingRequestUpdParam>())).Returns(new ResultInfo());

            var controller = CreateProcessingRequestController(processingRequestService.Object, sessionService: sessionService);

            //Action
            var result = controller.UpdateProcessingRequest(new ProcessingRequestUpdParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            //Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateProcessingRequest_WhenCallValidParamButNoPermission_ReturnError()
        {
            //Arrange
            var processingRequestService = new Mock<IProcessingRequestService>();
            processingRequestService.Setup(l => l.GetProcessingRequestDetail(It.IsAny<int>())).Returns(new ProcessingRequestDetailInfo { Status = ProcessingRequestStatus.DRAFT });

            var controller = CreateProcessingRequestController(processingRequestService.Object);

            //Action
            var result = controller.UpdateProcessingRequest(new ProcessingRequestUpdParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            //Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Not have permission", resultInfo.Message);
        }

        [Fact]
        public void UpdateProcessingRequest_WhenCallInvalidParam_ReturnError()
        {
            //Arrange
            var controller = CreateProcessingRequestController();
            controller.ModelState.AddModelError("Error", "Invalid");

            //Action
            var result = controller.UpdateProcessingRequest(new ProcessingRequestUpdParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            //Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Invalid", resultInfo.Message);
        }

        private ProcessingRequestController CreateProcessingRequestController(
            IProcessingRequestService processingRequestService = null,
 			ICommonSettingService commonSettingService = null,
            ISessionService sessionService = null
            )
        {
            var fakeProcessingRequestService = processingRequestService ?? Mock.Of<IProcessingRequestService>();
            var fakecommonSettingService = commonSettingService ?? Mock.Of<ICommonSettingService>();
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();

            return new ProcessingRequestController(fakeProcessingRequestService, fakecommonSettingService, fakeSessionService);
        }
    }
}
